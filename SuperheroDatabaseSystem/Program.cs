using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SuperheroDatabaseSystem
{
    // Data model for superhero
    public class Superhero
    {
        public string HeroId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Superpower { get; set; }
        public int ExamScore { get; set; }
        public string Rank { get; set; }
        public string ThreatLevel { get; set; }
    }

    // Professional color theme
    public static class HeroTheme
    {
        public static Color DarkBg = Color.FromArgb(10, 15, 30);
        public static Color CardBg = Color.FromArgb(20, 28, 50);
        public static Color LightText = Color.FromArgb(240, 245, 255);
        public static Color PrimaryBlue = Color.FromArgb(70, 150, 255);
        public static Color SecondaryRed = Color.FromArgb(255, 70, 100);
        public static Color GoldRank = Color.FromArgb(255, 200, 0);
        public static Color AccentPurple = Color.FromArgb(150, 100, 255);
        public static Color SuccessGreen = Color.FromArgb(50, 200, 100);
    }

    public partial class MainForm : Form
    {
        private const string HEROES_FILE = "superheroes.txt";
        private const string SUMMARY_FILE = "summary.txt";
        private List<Superhero> heroes = new List<Superhero>();

        // UI Controls
        private TextBox heroIdBox, nameBox, ageBox, superpowerBox, scoreBox;
        private Label rankLabel, threatLabel, scoreIndicator;
        private DataGridView heroGrid;
        private Label totalHeroesLabel, avgAgeLabel, avgScoreLabel;
        private Label sRankLabel, aRankLabel, bRankLabel, cRankLabel;
        private ProgressBar scoreBar;
        private Label selectedHeroLabel;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "ONE KICK HEROES ACADEMY";
            this.Size = new Size(1500, 900);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = HeroTheme.DarkBg;
            this.Font = new Font("Segoe UI", 10);
            this.DoubleBuffered = true;
            this.Icon = SystemIcons.Application;

            // Main layout
            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(0)
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 90));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            // Header
            Panel header = CreateHeader();
            mainLayout.Controls.Add(header, 0, 0);

            // Content area
            TableLayoutPanel content = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
                Padding = new Padding(15),
                BackColor = HeroTheme.DarkBg
            };
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 350));
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300));
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            // Left panel - Form inputs
            Panel formPanel = CreateFormPanel();
            content.Controls.Add(formPanel, 0, 0);

            // Middle panel - Statistics
            Panel statsPanel = CreateStatsPanel();
            content.Controls.Add(statsPanel, 1, 0);

            // Right panel - Data grid
            Panel gridPanel = CreateGridPanel();
            content.Controls.Add(gridPanel, 2, 0);

            mainLayout.Controls.Add(content, 0, 1);
            this.Controls.Add(mainLayout);

            LoadHeroes();
        }

        private Panel CreateHeader()
        {
            Panel header = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = HeroTheme.CardBg
            };

            // Gradient effect
            header.Paint += (s, e) =>
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    new Point(0, 0), new Point(0, header.Height),
                    Color.FromArgb(15, 25, 50), Color.FromArgb(25, 35, 60)))
                {
                    e.Graphics.FillRectangle(brush, header.ClientRectangle);
                }
                e.Graphics.DrawLine(new Pen(HeroTheme.PrimaryBlue, 3), 0, header.Height - 3, header.Width, header.Height - 3);
            };

            Label title = new Label
            {
                Text = "⚡ ONE KICK HEROES ACADEMY ⚡",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = HeroTheme.GoldRank,
                Location = new Point(20, 10),
                AutoSize = true
            };
            header.Controls.Add(title);

            Label subtitle = new Label
            {
                Text = "Advanced Superhero Management System | Real-Time Rank Calculation | Instant Analytics",
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = HeroTheme.PrimaryBlue,
                Location = new Point(20, 45),
                AutoSize = true
            };
            header.Controls.Add(subtitle);

            return header;
        }

        private Panel CreateFormPanel()
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = HeroTheme.CardBg,
                Padding = new Padding(15),
                AutoScroll = true
            };

            int y = 10;

            // Panel title
            Label formTitle = new Label
            {
                Text = "⚔️ HERO REGISTRY",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = HeroTheme.GoldRank,
                Location = new Point(5, y),
                AutoSize = true
            };
            panel.Controls.Add(formTitle);
            y += 35;

            // Hero ID
            AddFormField(panel, "HERO ID", out heroIdBox, ref y);

            // Name
            AddFormField(panel, "HERO NAME", out nameBox, ref y);

            // Age
            AddFormField(panel, "AGE", out ageBox, ref y);

            // Superpower
            AddFormField(panel, "SUPERPOWER", out superpowerBox, ref y);

            // Exam Score with indicator
            Label scoreLabel = new Label
            {
                Text = "EXAM SCORE",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = HeroTheme.LightText,
                Location = new Point(5, y),
                AutoSize = true
            };
            panel.Controls.Add(scoreLabel);

            scoreBox = new TextBox
            {
                Location = new Point(5, y + 22),
                Width = 320,
                Height = 32,
                BackColor = HeroTheme.DarkBg,
                ForeColor = HeroTheme.PrimaryBlue,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BorderStyle = BorderStyle.FixedSingle
            };
            scoreBox.TextChanged += ScoreBox_TextChanged;
            panel.Controls.Add(scoreBox);

            scoreBar = new ProgressBar
            {
                Location = new Point(5, y + 57),
                Width = 320,
                Height = 8,
                Minimum = 0,
                Maximum = 100,
                BackColor = HeroTheme.DarkBg
            };
            panel.Controls.Add(scoreBar);

            scoreIndicator = new Label
            {
                Text = "0/100",
                Font = new Font("Segoe UI", 8),
                ForeColor = HeroTheme.SecondaryRed,
                Location = new Point(5, y + 70),
                AutoSize = true
            };
            panel.Controls.Add(scoreIndicator);
            y += 100;

            // Rank display
            Label rankTitle = new Label
            {
                Text = "RANK",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = HeroTheme.LightText,
                Location = new Point(5, y),
                AutoSize = true
            };
            panel.Controls.Add(rankTitle);

            rankLabel = new Label
            {
                Text = "—",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = HeroTheme.GoldRank,
                Location = new Point(5, y + 22),
                Width = 320,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = HeroTheme.DarkBg,
                BorderStyle = BorderStyle.FixedSingle
            };
            panel.Controls.Add(rankLabel);
            y += 70;

            // Threat Level
            Label threatTitle = new Label
            {
                Text = "THREAT LEVEL",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = HeroTheme.LightText,
                Location = new Point(5, y),
                AutoSize = true
            };
            panel.Controls.Add(threatTitle);

            threatLabel = new Label
            {
                Text = "—",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = HeroTheme.SecondaryRed,
                Location = new Point(5, y + 22),
                Width = 320,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = HeroTheme.DarkBg,
                BorderStyle = BorderStyle.FixedSingle
            };
            panel.Controls.Add(threatLabel);
            y += 70;

            // Buttons
            AddButton(panel, "✓ ADD HERO", y, HeroTheme.SuccessGreen, () => AddHero());
            y += 42;
            AddButton(panel, "⟳ UPDATE HERO", y, HeroTheme.PrimaryBlue, () => UpdateHero());
            y += 42;
            AddButton(panel, "✕ DELETE HERO", y, HeroTheme.SecondaryRed, () => DeleteHero());
            y += 42;
            AddButton(panel, "⟲ CLEAR", y, Color.Gray, () => ClearFields());

            return panel;
        }

        private Panel CreateStatsPanel()
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = HeroTheme.CardBg,
                Padding = new Padding(15),
                AutoScroll = true
            };

            int y = 10;

            // Title
            Label title = new Label
            {
                Text = "📊 ANALYTICS",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = HeroTheme.PrimaryBlue,
                Location = new Point(5, y),
                AutoSize = true
            };
            panel.Controls.Add(title);
            y += 40;

            // Total Heroes
            AddStatField(panel, "TOTAL HEROES", out totalHeroesLabel, ref y);

            // Avg Age
            AddStatField(panel, "AVG AGE", out avgAgeLabel, ref y);

            // Avg Score
            AddStatField(panel, "AVG SCORE", out avgScoreLabel, ref y);

            y += 20;

            Label rankDistTitle = new Label
            {
                Text = "RANK DISTRIBUTION",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = HeroTheme.AccentPurple,
                Location = new Point(5, y),
                AutoSize = true
            };
            panel.Controls.Add(rankDistTitle);
            y += 32;

            // Rank counts
            AddRankStat(panel, "S-RANK", HeroTheme.GoldRank, ref y, out sRankLabel);
            AddRankStat(panel, "A-RANK", HeroTheme.PrimaryBlue, ref y, out aRankLabel);
            AddRankStat(panel, "B-RANK", HeroTheme.AccentPurple, ref y, out bRankLabel);
            AddRankStat(panel, "C-RANK", Color.Gray, ref y, out cRankLabel);

            y += 30;

            // Generate Report Button
            Button reportBtn = new Button
            {
                Text = "📄 GENERATE REPORT",
                Location = new Point(5, y),
                Width = 270,
                Height = 38,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = HeroTheme.AccentPurple,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            reportBtn.FlatAppearance.BorderSize = 0;
            reportBtn.Click += (s, e) => GenerateReport();
            panel.Controls.Add(reportBtn);

            return panel;
        }

        private Panel CreateGridPanel()
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = HeroTheme.DarkBg,
                Padding = new Padding(15)
            };

            // Header
            Panel headerPanel = new Panel
            {
                Height = 40,
                Dock = DockStyle.Top,
                BackColor = HeroTheme.CardBg
            };

            Label gridTitle = new Label
            {
                Text = "🦸 HERO DATABASE",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = HeroTheme.PrimaryBlue,
                Location = new Point(10, 8),
                AutoSize = true
            };
            headerPanel.Controls.Add(gridTitle);

            selectedHeroLabel = new Label
            {
                Text = "No hero selected",
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = HeroTheme.AccentPurple,
                Location = new Point(200, 10),
                AutoSize = true
            };
            headerPanel.Controls.Add(selectedHeroLabel);

            panel.Controls.Add(headerPanel);

            // Grid
            heroGrid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                BackgroundColor = HeroTheme.CardBg,
                GridColor = HeroTheme.DarkBg
            };

            heroGrid.DefaultCellStyle.BackColor = HeroTheme.CardBg;
            heroGrid.DefaultCellStyle.ForeColor = HeroTheme.LightText;
            heroGrid.DefaultCellStyle.SelectionBackColor = HeroTheme.PrimaryBlue;
            heroGrid.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            heroGrid.DefaultCellStyle.Padding = new Padding(3);

            heroGrid.ColumnHeadersDefaultCellStyle.BackColor = HeroTheme.PrimaryBlue;
            heroGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            heroGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            heroGrid.ColumnHeadersHeight = 30;

            heroGrid.SelectionChanged += HeroGrid_SelectionChanged;

            panel.Controls.Add(heroGrid);

            return panel;
        }

        private void AddFormField(Panel panel, string label, out TextBox textBox, ref int y)
        {
            Label labelControl = new Label
            {
                Text = label,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = HeroTheme.LightText,
                Location = new Point(5, y),
                AutoSize = true
            };
            panel.Controls.Add(labelControl);

            textBox = new TextBox
            {
                Location = new Point(5, y + 22),
                Width = 320,
                Height = 32,
                BackColor = HeroTheme.DarkBg,
                ForeColor = HeroTheme.PrimaryBlue,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BorderStyle = BorderStyle.FixedSingle
            };
            panel.Controls.Add(textBox);
            y += 62;
        }

        private void AddStatField(Panel panel, string label, out Label valueLabel, ref int y)
        {
            Label labelControl = new Label
            {
                Text = label + ":",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = HeroTheme.LightText,
                Location = new Point(5, y),
                AutoSize = true
            };
            panel.Controls.Add(labelControl);

            valueLabel = new Label
            {
                Text = "0",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = HeroTheme.GoldRank,
                Location = new Point(5, y + 22),
                Width = 270,
                Height = 32,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = HeroTheme.DarkBg,
                BorderStyle = BorderStyle.FixedSingle
            };
            panel.Controls.Add(valueLabel);
            y += 62;
        }

        private void AddRankStat(Panel panel, string rank, Color color, ref int y, out Label countLabel)
        {
            Panel rankPanel = new Panel
            {
                Location = new Point(5, y),
                Width = 270,
                Height = 30,
                BackColor = HeroTheme.DarkBg,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label rankLabel = new Label
            {
                Text = rank,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = color,
                Location = new Point(5, 5),
                AutoSize = true
            };
            rankPanel.Controls.Add(rankLabel);

            countLabel = new Label
            {
                Text = "0",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = color,
                Location = new Point(230, 5),
                AutoSize = true
            };
            rankPanel.Controls.Add(countLabel);

            panel.Controls.Add(rankPanel);
            y += 36;
        }

        private void AddButton(Panel panel, string text, int y, Color color, Action onClick)
        {
            Button btn = new Button
            {
                Text = text,
                Location = new Point(5, y),
                Width = 320,
                Height = 36,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (s, e) => onClick?.Invoke();
            btn.MouseEnter += (s, e) => btn.BackColor = ControlPaint.Light(color, 0.15f);
            btn.MouseLeave += (s, e) => btn.BackColor = color;

            Panel parent = (Panel)btn.Parent ?? new Panel();
            foreach (Control c in panel.Controls.OfType<Button>())
            {
                if (c.Location.Y == y) return;
            }
            panel.Controls.Add(btn);
        }

        private void ScoreBox_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(scoreBox.Text, out int score))
            {
                score = Math.Max(0, Math.Min(100, score));
                scoreBox.Text = score.ToString();
                scoreBar.Value = score;
                scoreIndicator.Text = $"{score}/100";

                string rank = GetRank(score);
                string threat = GetThreat(rank);

                rankLabel.Text = rank;
                threatLabel.Text = threat;

                // Color coding
                if (score >= 81) scoreIndicator.ForeColor = HeroTheme.GoldRank;
                else if (score >= 61) scoreIndicator.ForeColor = HeroTheme.PrimaryBlue;
                else if (score >= 41) scoreIndicator.ForeColor = HeroTheme.AccentPurple;
                else scoreIndicator.ForeColor = HeroTheme.SecondaryRed;
            }
        }

        private void HeroGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (heroGrid.SelectedRows.Count > 0)
            {
                DataGridViewRow row = heroGrid.SelectedRows[0];
                heroIdBox.Text = row.Cells[0].Value?.ToString() ?? "";
                nameBox.Text = row.Cells[1].Value?.ToString() ?? "";
                ageBox.Text = row.Cells[2].Value?.ToString() ?? "";
                superpowerBox.Text = row.Cells[3].Value?.ToString() ?? "";
                scoreBox.Text = row.Cells[4].Value?.ToString() ?? "";
                selectedHeroLabel.Text = $"Selected: {nameBox.Text}";
            }
        }

        private void AddHero()
        {
            if (!ValidateInput()) return;

            int age = int.Parse(ageBox.Text);
            int score = int.Parse(scoreBox.Text);

            if (heroes.Any(h => h.HeroId == heroIdBox.Text))
            {
                MessageBox.Show("❌ Hero ID already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Superhero hero = new Superhero
            {
                HeroId = heroIdBox.Text,
                Name = nameBox.Text,
                Age = age,
                Superpower = superpowerBox.Text,
                ExamScore = score,
                Rank = GetRank(score),
                ThreatLevel = GetThreat(GetRank(score))
            };

            heroes.Add(hero);
            SaveHeroes();
            RefreshGrid();
            ClearFields();
            MessageBox.Show("✓ Hero added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateHero()
        {
            if (string.IsNullOrWhiteSpace(heroIdBox.Text))
            {
                MessageBox.Show("⚠ Select a hero to update!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput()) return;

            Superhero hero = heroes.FirstOrDefault(h => h.HeroId == heroIdBox.Text);
            if (hero == null)
            {
                MessageBox.Show("❌ Hero not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int age = int.Parse(ageBox.Text);
            int score = int.Parse(scoreBox.Text);

            hero.Name = nameBox.Text;
            hero.Age = age;
            hero.Superpower = superpowerBox.Text;
            hero.ExamScore = score;
            hero.Rank = GetRank(score);
            hero.ThreatLevel = GetThreat(hero.Rank);

            SaveHeroes();
            RefreshGrid();
            MessageBox.Show("✓ Hero updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DeleteHero()
        {
            if (string.IsNullOrWhiteSpace(heroIdBox.Text))
            {
                MessageBox.Show("⚠ Select a hero to delete!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Delete this hero? This cannot be undone.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Superhero hero = heroes.FirstOrDefault(h => h.HeroId == heroIdBox.Text);
                if (hero != null)
                {
                    heroes.Remove(hero);
                    SaveHeroes();
                    RefreshGrid();
                    ClearFields();
                    MessageBox.Show("✓ Hero deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ClearFields()
        {
            heroIdBox.Clear();
            nameBox.Clear();
            ageBox.Clear();
            superpowerBox.Clear();
            scoreBox.Clear();
            rankLabel.Text = "—";
            threatLabel.Text = "—";
            scoreBar.Value = 0;
            scoreIndicator.Text = "0/100";
            selectedHeroLabel.Text = "No hero selected";
        }

        private void GenerateReport()
        {
            if (heroes.Count == 0)
            {
                MessageBox.Show("Add heroes first!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int total = heroes.Count;
            double avgAge = heroes.Average(h => h.Age);
            double avgScore = heroes.Average(h => h.ExamScore);
            int sCount = heroes.Count(h => h.Rank == "S-Rank");
            int aCount = heroes.Count(h => h.Rank == "A-Rank");
            int bCount = heroes.Count(h => h.Rank == "B-Rank");
            int cCount = heroes.Count(h => h.Rank == "C-Rank");

            totalHeroesLabel.Text = total.ToString();
            avgAgeLabel.Text = avgAge.ToString("F1");
            avgScoreLabel.Text = avgScore.ToString("F1");
            sRankLabel.Text = sCount.ToString();
            aRankLabel.Text = aCount.ToString();
            bRankLabel.Text = bCount.ToString();
            cRankLabel.Text = cCount.ToString();

            string report = $"ONE KICK HEROES ACADEMY - REPORT\n" +
                $"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n\n" +
                $"SUMMARY:\nTotal: {total} | Avg Age: {avgAge:F1} | Avg Score: {avgScore:F1}\n\n" +
                $"RANKS:\nS-Rank: {sCount}\nA-Rank: {aCount}\nB-Rank: {bCount}\nC-Rank: {cCount}\n";

            File.WriteAllText(SUMMARY_FILE, report);
            MessageBox.Show("✓ Report generated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(heroIdBox.Text) || string.IsNullOrWhiteSpace(nameBox.Text) ||
                string.IsNullOrWhiteSpace(ageBox.Text) || string.IsNullOrWhiteSpace(superpowerBox.Text) ||
                string.IsNullOrWhiteSpace(scoreBox.Text))
            {
                MessageBox.Show("Fill all fields!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(ageBox.Text, out int age) || age < 0 || age > 120)
            {
                MessageBox.Show("Age must be 0-120!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(scoreBox.Text, out int score) || score < 0 || score > 100)
            {
                MessageBox.Show("Score must be 0-100!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private string GetRank(int score)
        {
            if (score >= 81) return "S-Rank";
            if (score >= 61) return "A-Rank";
            if (score >= 41) return "B-Rank";
            return "C-Rank";
        }

        private string GetThreat(string rank)
        {
            switch (rank)
            {
                case "S-Rank":
                    return "Finals Week";
                case "A-Rank":
                    return "Midterm Madness";
                case "B-Rank":
                    return "Group Project Gone Wrong";
                default:
                    return "Pop Quiz";
            }
        }

        private void LoadHeroes()
        {
            heroes.Clear();
            if (File.Exists(HEROES_FILE))
            {
                foreach (string line in File.ReadAllLines(HEROES_FILE))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    string[] parts = line.Split('|');
                    if (parts.Length == 7 && int.TryParse(parts[2], out int age) && int.TryParse(parts[4], out int score))
                    {
                        heroes.Add(new Superhero
                        {
                            HeroId = parts[0],
                            Name = parts[1],
                            Age = age,
                            Superpower = parts[3],
                            ExamScore = score,
                            Rank = parts[5],
                            ThreatLevel = parts[6]
                        });
                    }
                }
            }
            RefreshGrid();
        }

        private void SaveHeroes()
        {
            using (StreamWriter writer = new StreamWriter(HEROES_FILE, false))
            {
                foreach (Superhero hero in heroes)
                {
                    writer.WriteLine($"{hero.HeroId}|{hero.Name}|{hero.Age}|{hero.Superpower}|{hero.ExamScore}|{hero.Rank}|{hero.ThreatLevel}");
                }
            }
        }

        private void RefreshGrid()
        {
            heroGrid.DataSource = null;
            heroGrid.DataSource = heroes;

            if (heroGrid.Columns.Count > 0)
            {
                heroGrid.Columns[0].HeaderText = "ID";
                heroGrid.Columns[1].HeaderText = "NAME";
                heroGrid.Columns[2].HeaderText = "AGE";
                heroGrid.Columns[3].HeaderText = "SUPERPOWER";
                heroGrid.Columns[4].HeaderText = "SCORE";
                heroGrid.Columns[5].HeaderText = "RANK";
                heroGrid.Columns[6].HeaderText = "THREAT LEVEL";

                foreach (DataGridViewColumn col in heroGrid.Columns)
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}