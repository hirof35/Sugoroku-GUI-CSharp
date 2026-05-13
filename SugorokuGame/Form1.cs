using System.Drawing;
using System.Text.Json;

namespace SugorokuGame
{
    public class Player
    {
        public string Name { get; set; }
        public char Symbol { get; set; }
        public int Position { get; set; } = 0;
        public bool IsSkipping { get; set; } = false;
        public Color PlayerColor { get; set; }
        public bool IsAI { get; set; }
    }

    public class MapEvent
    {
        public string Name { get; set; }
        public int Move { get; set; }
        public bool Skip { get; set; }
    }

    public partial class Form1 : Form
    {
        const int Goal = 30;
        const string SaveFile = "sugoroku_save.json";
        readonly Random rand = new Random();
        List<Player> players;
        int turnIndex = 0;

        static readonly Dictionary<int, MapEvent> Events = new Dictionary<int, MapEvent>
        {
            { 5,  new MapEvent { Name = "急加速！", Move = 3 } },
            { 12, new MapEvent { Name = "落とし穴！", Move = -3 } },
            { 18, new MapEvent { Name = "バナナの皮", Skip = true } },
            { 25, new MapEvent { Name = "ワープ！", Move = 4 } }
        };

        public Form1()
        {
            InitializeComponent(); // デザイナーの配置を読み込む
            SetupGame();
            UpdateDisplay();
        }

        private void SetupGame()
        {
            if (File.Exists(SaveFile))
            {
                try
                {
                    string json = File.ReadAllText(SaveFile);
                    players = JsonSerializer.Deserialize<List<Player>>(json);
                    AddLog("💾 セーブデータを復元しました。");
                    return;
                }
                catch { AddLog("⚠️ セーブ失敗、新規開始します。"); }
            }

            players = new List<Player> {
                new Player { Name = "Player", Symbol = 'P', PlayerColor = Color.DeepSkyBlue, IsAI = false },
                new Player { Name = "CPU", Symbol = 'C', PlayerColor = Color.Crimson, IsAI = true }
            };
        }

        private void buttonDice_Click(object sender, EventArgs e)
        {
            Player p = players[turnIndex];
            if (p.IsSkipping)
            {
                MessageBox.Show($"{p.Name}はお休みです");
                p.IsSkipping = false;
                NextTurn();
                return;
            }

            int dice = rand.Next(1, 7);
            p.Position += dice;
            if (p.Position > Goal) p.Position = Goal;

            string msg = $"【{p.Name}】: {dice}が出た ➔ {p.Position}マス";

            if (Events.TryGetValue(p.Position, out var ev))
            {
                msg += $"\n✨ {ev.Name}！";
                p.Position = Math.Max(0, Math.Min(p.Position + ev.Move, Goal));
                if (ev.Skip) p.IsSkipping = true;
            }

            labelStatus.Text = msg;
            AddLog(msg);
            UpdateDisplay();

            if (p.Position >= Goal)
            {
                MessageBox.Show($"{p.Name}がゴール！");
                buttonDice.Enabled = false;
                return;
            }
            NextTurn();
        }

        private void NextTurn()
        {
            turnIndex = (turnIndex + 1) % players.Count;
            this.Text = $"すごろく - {players[turnIndex].Name}の番";
        }

        private void UpdateDisplay()
        {
            // マップの簡易表示
            string map = "  [";
            for (int i = 0; i <= Goal; i++)
            {
                var pPos = players.Where(pl => pl.Position == i).ToList();
                if (pPos.Any()) map += pPos.First().Symbol;
                else if (i == Goal) map += "G";
                else map += "-";
            }
            txtMap.AppendText(map + "]\n");
        }

        private void AddLog(string text)
        {
            txtMap.SelectionStart = txtMap.TextLength;
            txtMap.SelectionColor = players[turnIndex].PlayerColor;
            txtMap.AppendText($"> {text}\n");
            txtMap.ScrollToCaret();
        }
    }

}
