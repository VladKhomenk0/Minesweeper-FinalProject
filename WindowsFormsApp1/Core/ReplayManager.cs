using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Core.Commands;

namespace WindowsFormsApp1.Core
{
    public class ReplayManager
    {
        private List<ICellCommand> _commands = new List<ICellCommand>();
        private GameMemento _initialState;
        private Game _game;
        private GameRenderer _renderer;
        private Label _timerLabel;

        public bool IsReplaying { get; private set; }

        public ReplayManager(Game game, GameRenderer renderer, Label timerLabel)
        {
            _game = game;
            _renderer = renderer;
            _timerLabel = timerLabel;
        }

        public void SetInitialState(GameMemento state)
        {
            _initialState = state;
        }

        public void AddCommand(ICellCommand command)
        {
            if (!IsReplaying)
            {
                _commands.Add(command);
            }
        }

        public async Task PlayReplayAsync()
        {
            if (_initialState == null || _commands.Count == 0) return;

            IsReplaying = true;

            // Відновлення чистого поля (до першого кліку)
            _game.RestoreState(_initialState);
            _renderer.RedrawBoard();

            // Відтворення всіх збережених команд з фіксованою затримкою
            foreach (var cmd in _commands)
            {
                await Task.Delay(400);

                var changed = cmd.Execute();
                if (changed != null && changed.Count > 0)
                {
                    _renderer.RedrawCells(changed);
                }
            }

            MessageBox.Show("Повтор завершено.", "Replay", MessageBoxButtons.OK, MessageBoxIcon.Information);
            IsReplaying = false;
        }
    }
}