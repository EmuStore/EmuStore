using System.Collections.Generic;
using Godot;
using EmuStore.Models;
using EmuStore.Services;

public partial class root : Control
{
    private LineEdit tokenInput;
    private OptionButton gameSelect;
    private List<Game> games;

    public override void _Ready()
    {
        tokenInput = GetNode<LineEdit>("TokenInput");
        gameSelect = GetNode<OptionButton>("GameSelect");
    }

    public async void OnScanGamesPressed()
    {
        GD.Print("Scanning...");
        GameService gameService = new GameService();
        games = await gameService.GetAllGames(tokenInput.Text);
        GD.Print("Found ", games.Count, " games");
        if (games.Count > 0)
        {
            gameSelect.Clear();
            games.ForEach(
                (game) =>
                {
                    gameSelect.AddItem(game.Name, game.Id);
                }
            );
        }
    }

    public void OnDownloadPressed()
    {
        GD.Print("Downloading...");
        Game game = games.Find((item) => item.Id == gameSelect.GetSelectedId());
        GameService gameService = new GameService();
        GD.Print(game.Name);
        GD.Print(game.Platform);
        gameService.DownloadGame(game, tokenInput.Text);
    }
}
