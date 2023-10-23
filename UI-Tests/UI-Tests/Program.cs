using Microsoft.Playwright;

namespace UI_Tests
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // startar playwright
            using var playwright = await Playwright.CreateAsync();

            // gör detta för att kunna köra playwright med microsoft edge
            var edgeExecutablePath = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";  // This is a common default path, adjust if necessary

            await using var browser = await playwright.Chromium.LaunchAsync(new()
            {
                Headless = true,
                ExecutablePath = edgeExecutablePath
            });

            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();

            await page.GotoAsync("https://localhost:placeholder/");

            var placeHolder = await page.QuerySelectorAllAsync("med den här får man bara ut inre text från id på hemsidan");

            var placeHolderForPageText = placeHolder[0].InnerTextAsync();


            Console.WriteLine($"nånting händer kanske skriva vad");
            Console.ReadLine();
        }
    }
}
