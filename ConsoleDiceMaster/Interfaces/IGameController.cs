
using DiceMasters.Classes;

namespace DiceMasters.Interfaces;

public interface IGameController
{
    /// <summary>
    /// Initializes the game and loads saved data
    /// </summary>
    GameData LoadGame();
    
    /// <summary>
    /// Starts the game after the user presses the associated key
    /// </summary>
    /// <param name="keyInfo">Key pressed sent to the command line</param>
    void StartGame();
    
    /// <summary>
    /// Quits the game/application when the user presses the associated key
    /// </summary>
    /// <param name="keyInfo">Key pressed sent to the command line</param>
    void QuitGame();
}
