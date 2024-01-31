using System;
using System.IO;
using System.Threading.Tasks;
using Azure.AI.OpenAI;

namespace Azure.AI.OpenAI.Tests.Samples
{
    public partial class GenerateImages
    {
        // add an async Main method:
        public static async Task Main(string[] args)
        {
            string? endpoint = Environment.GetEnvironmentVariable("OpenAI_ENDPOINT");
            string? key = Environment.GetEnvironmentVariable("OpenAI_KEY");

            if (string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(key))
            {
                Console.WriteLine("OpenAI_ENDPOINT or OpenAI_KEY environment variables not set.");
                return;
            }

            OpenAIClient client = new(new Uri(endpoint), new AzureKeyCredential(key));

            Response<ImageGenerations> imageGenerations = await client.GetImageGenerationsAsync(
                new ImageGenerationOptions()
                {
                    Prompt = "a cat",
                    Size = ImageSize.Size512x512,
                });

            // Image Generations responses provide URLs you can use to retrieve requested images
            Uri imageUri = imageGenerations.Value.Data[0].Url;

            // Print the image URI to console:
            Console.WriteLine(imageUri);
        }
    }
}