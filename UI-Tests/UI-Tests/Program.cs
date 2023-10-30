using Microsoft.Playwright;

namespace UI_Tests
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Test has begun");

                // startar playwright
                using var playwright = await Playwright.CreateAsync();

                // detta är bara för att använda microsoft edge (funkar bättre enligt mig)
                var edgeExecutablePath = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";

                await using var browser = await playwright.Chromium.LaunchAsync(new()
                {
                    Headless = true,
                    ExecutablePath = edgeExecutablePath
                });

                var context = await browser.NewContextAsync();
                var page = await context.NewPageAsync();

                await page.GotoAsync("http://localhost:3000/");

                // klickar på starta spelet
                await page.ClickAsync("#startgame-button");
                await page.WaitForNavigationAsync();
                Console.WriteLine("Clicked start game button");

                // skriver ut attack power innan svärdet är på
                var startattackpower = await page.InnerTextAsync("#attackpower-tag");
                Console.WriteLine("Attack power before equip: " + startattackpower + "'.");

                // kollar inventory
                await page.ClickAsync("#checkinventory-button");
                await page.WaitForSelectorAsync("#equip-button");
                Console.WriteLine("Clicked check inventory button");

                // sätter på svärdet
                await page.ClickAsync("#equip-button");
                await page.WaitForTimeoutAsync(1000);
                Console.WriteLine("Clicked equip button");

                var attackPower = await page.InnerTextAsync("#attackpower-tag");
                if (attackPower != "Attack Power: 3")
                {
                    Console.WriteLine("Test failed: Expected attack power to be '3', but found '" + attackPower + "'.");
                    return;
                }
                else
                {
                    Console.WriteLine("Test passed: Attack power has increased to 3");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            finally
            {
                Console.WriteLine("Test completed successfully!");
                Console.ReadLine();
            }
        }
    }
}
