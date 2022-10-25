using System.IO;
using System.Text;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using EmuStore.Models;

namespace EmuStore.Services
{
    public class GameService
    {
        public async Task<List<Game>> GetAllGames(string token)
        {
            try
            {
                HttpClient client = UtilityService.GetHttpClient(token);
                HttpResponseMessage response = await client.GetAsync("/game/list");
                response.EnsureSuccessStatusCode();
                string respStr = await response.Content.ReadAsStringAsync();
                List<Game> games = UtilityService.DeserializeJson<List<Game>>(respStr);
                return games;
            }
            catch (Exception ex)
            {
                GD.Print(ex);
                return new List<Game>();
            }
        }

        public void DownloadGame(Game game, string token)
        {
            try
            {
                HttpClient client = UtilityService.GetHttpClient(token);
                StringBuilder urlParams = new StringBuilder();
                urlParams.Append("?name=" + Uri.EscapeDataString(game.Name));
                urlParams.Append("&platform=" + Uri.EscapeDataString(game.Platform));
                System.Threading.Thread thread = new System.Threading.Thread(async () =>
                {
                    var response = await client.GetStreamAsync(
                        "/game/download" + urlParams.ToString()
                    );
                    var downloadDir = "Your Download Path";
                    DirectoryInfo info = new DirectoryInfo(downloadDir);
                    if (!info.Exists)
                    {
                        info.Create();
                    }
                    string path = System.IO.Path.Combine(downloadDir, game.FileName);
                    FileStream outputFileStream = new FileStream(path, FileMode.Create);
                    response.CopyTo(outputFileStream);

                    FileInfo fileInfo = new FileInfo(path);
                    GD.Print("Original Size: ", game.Size, " bytes");
                    GD.Print("Copied Size: ", fileInfo.Length, " bytes");
                });
                thread.Start();
                GD.Print("Done");
            }
            catch (Exception ex)
            {
                GD.Print(ex);
            }
        }
    }
}
