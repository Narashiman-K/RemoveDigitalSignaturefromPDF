using GdPicture14;
using System;
using System.IO;

namespace PDFToImageToPDF
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize GDPicture license
            LicenseManager lm = new LicenseManager();
            lm.RegisterKEY("........ARHVoez6xrhe24gbHQ="); // Replace with your actual license key or Demo licenekey

            Console.WriteLine("PDF Signature Removal and Processing Tool");
            Console.WriteLine("========================================");

            // Get input PDF file path
            string inputPdfPath = GetInputFilePath();
            if (string.IsNullOrEmpty(inputPdfPath)) return;

            // Get output PDF file path
            string outputPdfPath = GetOutputFilePath(inputPdfPath);

            // Process PDF by removing signatures
            bool success = ProcessPDFRemoveSignatures(inputPdfPath, outputPdfPath);

            Console.WriteLine(success
                ? $"\nProcessing completed successfully!{Environment.NewLine}Output file: {outputPdfPath}"
                : "\nProcessing failed!");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static string GetInputFilePath()
        {
            Console.WriteLine("Enter the path to your PDF file:");
            string inputPath = Console.ReadLine()?.Trim('"');

            if (string.IsNullOrEmpty(inputPath) || !File.Exists(inputPath))
            {
                Console.WriteLine("Invalid file path or file does not exist.");
                return null;
            }
            if (!inputPath.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Please provide a PDF file.");
                return null;
            }
            return inputPath;
        }

        static string GetOutputFilePath(string inputPath)
        {
            string dir = Path.GetDirectoryName(inputPath);
            string name = Path.GetFileNameWithoutExtension(inputPath);
            string suggested = Path.Combine(dir, $"{name}_ProcessedNoSignatures.pdf");

            Console.WriteLine($"Output will be saved as: {suggested}");
            Console.WriteLine("Press Enter to accept or type a new path:");
            string user = Console.ReadLine()?.Trim('"');
            return string.IsNullOrEmpty(user) ? suggested : user;
        }

        static bool ProcessPDFRemoveSignatures(string inputPdfPath, string outputPdfPath)
        {
            using (GdPicturePDF opdf = new GdPicturePDF())
            {
                Console.WriteLine("Loading PDF file...");

                // Load the PDF file
                GdPictureStatus status = opdf.LoadFromFile(inputPdfPath);
                if (status != GdPictureStatus.OK)
                {
                    Console.WriteLine($"Error loading PDF: {status}");
                    return false;
                }

                Console.WriteLine("PDF loaded successfully.");

                // Check for signatures
                var nbSignatures = opdf.GetSignatureCount();
                Console.WriteLine($"Found {nbSignatures} signature(s) in the document.");

                if (nbSignatures > 0)
                {
                    Console.WriteLine("Removing signatures...");

                    // Remove all signatures (starting from the last one)
                    for (int i = nbSignatures - 1; i >= 0; i--)
                    {
                        Console.Write($"Removing signature {i + 1}/{nbSignatures}... ");

                        status = opdf.RemoveSignature(i);
                        if (status == GdPictureStatus.OK)
                        {
                            Console.WriteLine("Done");
                        }
                        else
                        {
                            Console.WriteLine($"Error: {status}");
                        }
                    }

                    // Verify signatures are removed
                    int remainingSignatures = opdf.GetSignatureCount();
                    if (remainingSignatures == 0)
                    {
                        Console.WriteLine("All signatures removed successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Warning: {remainingSignatures} signature(s) still remain.");
                    }
                }
                else
                {
                    Console.WriteLine("No signatures found in the document. Processing anyway...");
                }

                Console.WriteLine("Saving processed PDF...");

                // Save the PDF without signatures
                status = opdf.SaveToFile(outputPdfPath);
                if (status == GdPictureStatus.OK)
                {
                    Console.WriteLine("Document saved successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Error saving the document: {status}");
                    return false;
                }
            }
        }
    }
}