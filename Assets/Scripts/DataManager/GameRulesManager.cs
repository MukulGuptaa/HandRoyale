using System.Collections.Generic;
using UI;

namespace DataManager
{
    public class GameRulesManager
    {
        /// <summary>
        /// Class that manages the game rules logic.
        /// </summary>
        private readonly Dictionary<Move, List<Move>> _winConditions = new()
        {
            { Move.Rock, new List<Move> { Move.Scissors, Move.Lizard } },
            { Move.Paper, new List<Move> { Move.Rock, Move.Spock } },
            { Move.Scissors, new List<Move> { Move.Paper, Move.Lizard } },
            { Move.Lizard, new List<Move> { Move.Paper, Move.Spock } },
            { Move.Spock, new List<Move> { Move.Rock, Move.Scissors } }
        };

        public GameResultStatus GetGameResultStatus(Move player, Move opponent)
        {
            if (player == Move.None) {
                return GameResultStatus.LOSS_BY_TIMEOUT;
            } 
            if (player == opponent) {
                return GameResultStatus.TIE;
            }
            
            return _winConditions[player].Contains(opponent) ? GameResultStatus.WON : GameResultStatus.LOSS;
        }
    }
}
