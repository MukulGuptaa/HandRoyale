using System;
using DataManager;

namespace Util
{
    /// <summary>
    /// Game Events class that holds the different events
    /// </summary>
    public class GameEvents
    {
        public static Action<Move> OnPlayerMoveChoiceSelected;
        public static void RaiseOnPlayerMoveChoiceSelected(Move move)
        {
            OnPlayerMoveChoiceSelected?.Invoke(move);
        }

        public static Action OnAiMoveChoiceShownInUi;

        public static void RaiseOnAiMoveChoiceShownInUi()
        {
            OnAiMoveChoiceShownInUi?.Invoke();
        }
        
    }
}