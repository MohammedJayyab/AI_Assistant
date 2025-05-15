using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms; // Requires adding a reference to System.Windows.Forms

namespace AI_Assistant;

public static class ImageHelper
{
    /// <summary>
    /// Captures an image from the clipboard, saves it as a JPG file
    /// with a unique ID in the specified directory, and returns the full path.
    /// </summary>
    /// <param name="directoryPath">The directory where the image will be saved.</param>
    /// <returns>The full path of the saved image file, or null if no image was found on the clipboard or an error occurred.</returns>
    public static string SaveClipboardImageAsJpg(string directoryPath)
    {
        // Ensure the directory exists
        if (!Directory.Exists(directoryPath))
        {
            try
            {
                Directory.CreateDirectory(directoryPath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating directory {directoryPath}: {ex.Message}");
            }
        }

        // Check if the clipboard contains image data
        if (Clipboard.ContainsImage())
        {
            try
            {
                // Get the image from the clipboard
                Image clipboardImage = Clipboard.GetImage();

                if (clipboardImage != null)
                {
                    // Generate a unique filename using a GUID
                    string uniqueFileName = Guid.NewGuid().ToString() + ".jpg";

                    // Combine directory path and filename
                    string fullPath = Path.Combine(directoryPath, uniqueFileName);

                    // Save the image as JPG
                    // Use the 'using' statement to ensure the image object is disposed
                    using (clipboardImage)
                    {
                        clipboardImage.Save(fullPath, ImageFormat.Jpeg);
                    }

                    return fullPath;
                }
                else
                {
                    throw new Exception("Clipboard contains image data, but GetImage() returned null.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving clipboard image: {ex.Message}");
            }
        }
        else
        {
            return null;
        }
    }
}