using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace DowntownCarCounter
{
    public partial class frmCarCountClassifier : Form
    {
        public frmCarCountClassifier()
        {
            InitializeComponent();
            btnPause.Enabled = false;
        }

        List<Blip> blips = null;
        bool paintBlips = false;
        bool paintTracks = false;

        List<Target> targets = null;

        double currentTime = 0;
        bool loaded = false;

        private void btnStart_Click(object sender, EventArgs e)
        {
            loaded = false;
            selectedTarget = -1;
            btnStart.Enabled = false;
            btnPause.Enabled = true;

            paused = true;
            
            lvTargets.Items.Clear();
            lvTargets.Columns.Clear();
            lvTargets.Columns.Add("Target", 60);
            lvTargets.Columns.Add("Blips", 40);
            lvTargets.Columns.Add("Avg. Vel.", 150);
            lvTargets.Columns.Add("Start time.", 90);
            lvTargets.Columns.Add("End time", 90);

            try
            {
                #region Load Blips form blobs.csv
                string filename = "blobs.csv";
                string dir = Path.GetDirectoryName(Application.ExecutablePath);
                string path = Path.Combine(dir, filename);

                if (!File.Exists(path))
                {
                    MessageBox.Show(String.Format("Unable to locate {0} in\n{1}", filename, dir));
                    return;
                }

                blips = new List<Blip>();

                int lineNum = 1; //we already read one
                string line = "";
                try
                {
                    string[] lines = File.ReadAllLines(path);
                    foreach(string line2 in lines)
                    {
                        if (line2.StartsWith("time"))
                            continue;

                        line = line2;

                        
                        lineNum++;
                        string[] parts = line.Split(new char[] { ',' }, 5);
                        int time = int.Parse(parts[0]);
                        double x = double.Parse(parts[1]);
                        double y = double.Parse(parts[2]);
                        int area = int.Parse(parts[3]);
                        int id = int.Parse(parts[4]);


                        Blip blip = new Blip(x, y, time, area, id);
                        blips.Add(blip);

                        if (paintBlips)
                        {
                            myDisplay1.Invalidate();
                            Application.DoEvents();
                            System.Threading.Thread.Sleep(10);
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("Caught exception {0} at line {1} in file {2}.\nLine contents: {3}", ex.Message, lineNum, filename, line));
                }

                Trace.WriteLine("Finished loading Blips...");
#endregion

                int targetID = 0;
                int count = 0;

                try
                {
                    targets = new List<Target>();
                    paintTracks = true;

                    foreach(Blip b in blips)
                    {

                        while (paused && !step)
                        {
                            Application.DoEvents();
                            System.Threading.Thread.Sleep(100);
                        }
                        step = false;

                        currentTime = b.time;
                        tbTime.Value = (int)(currentTime);

                        Target bestTarget = null;
                        double bestScore = double.MaxValue;

                        if (b.time > 3.4)
                        {

                        }

                        //Target movedFurthest = null;
                        foreach (Target t in targets)
                        {
                            //if (movedFurthest == null || movedFurthest.AverageVel == null || t.AverageVel != null && t.AverageVel.DistanceTravelled > movedFurthest.AverageVel.DistanceTravelled)
                            //    if (currentTime - t.lastBlip.time < 30)
                            //        movedFurthest = t;

                            double score = t.TestNewBlip(b);
                            if (score < bestScore)
                            {
                                bestScore = score;
                                bestTarget = t;
                            }
                        }

                        //if (movedFurthest != null)
                        //    selectedTarget = targets.IndexOf(movedFurthest);

                        if (bestScore == double.MaxValue)
                        {
                            targets.Add(new Target(b, targetID++));
                        }
                        else
                        {
                            bestTarget.AddBlip(b);
                            bestTarget.AverageVel.ToString();

                            //myDisplay1.Invalidate();

                            //if (++count > 200)
                            //{
                            //    count = 0;
                            //    RepopulateListView();
                            //}

                            Application.DoEvents();
                        }

                        myDisplay1.Invalidate();
                        //RepopulateListView();
                        Application.DoEvents();


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("Caught exception {0}", ex.Message));
                }

            }
            finally
            {
                btnStart.Enabled = true;
                btnPause.Enabled = false;
                paused = false;
                loaded = true;
                paintTracks = true;

                RepopulateListView();
                myDisplay1.Invalidate();
                Application.DoEvents();
            }
        }

        private void RepopulateListView()
        {
            try
            {
                autoUpdate = true;
                lvTargets.BeginUpdate();
                lvTargets.Items.Clear();
                int tgtIndex = -1;
                int idx = 0;

                foreach (Target t in targets)
                {
                    

                    ListViewItem lvi = new ListViewItem(t.targetID.ToString());
                    lvi.SubItems.Add(t.Blips.Count.ToString());
                    lvi.SubItems.Add(t.AverageVel.ToString());
                    lvi.SubItems.Add(t.firstBlip.time.ToString());
                    lvi.SubItems.Add(t.lastBlip.time.ToString());

                    
                    if (t.Blips.Count > 3)
                    {
                        if (t.targetID == selectedTarget)
                            tgtIndex = idx;

                        lvTargets.Items.Add(lvi);
                        idx++;
                    }

                }

                if (tgtIndex >= 0)
                {
                    try
                    {
                        lvTargets.Items[tgtIndex].Selected = true;
                        lvTargets.EnsureVisible(tgtIndex);
                    }
                    catch (Exception ex)
                    {


                    }
                }

            }
            finally
            {
                lvTargets.EndUpdate();
                autoUpdate = false;
            }
        }



        private void pnlRoad_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int blipRadius = 4;

            #region Paint Blips
            if (paintBlips)
            {
                int toShow = 30;
                int alphaChange = 200;

                
                g.FillRectangle(Brushes.White, e.ClipRectangle);

                if (blips == null)
                    return;

                int start = blips.Count - toShow;
                if (start < 0)
                    start = 0;

                for (int i = start; i < blips.Count; i++)
                {
                    Blip b = blips[i];
                    using (Brush br = new SolidBrush(Color.FromArgb(255 - alphaChange * (blips.Count - 1 - i) / toShow, Color.Black)))
                    {
                        g.FillEllipse(br, (float)b.X - blipRadius, (float)b.Y - blipRadius, 2 * blipRadius, 2 * blipRadius);
                    }
                }
            }
            #endregion
        

        
            #region Paint Tracks
            if(paintTracks)
            {
                const int MIN_BLIPS = 1;

                g.FillRectangle(Brushes.White, e.ClipRectangle);

                foreach(Target t in targets)
                //if(targets.Count > 0)
                {
                  //  Target t = targets[0];

                    try
                    {
                        if (t.Blips.Count >= MIN_BLIPS)
                        {
                            Color c;
                            if (t.Blips.Count < MIN_BLIPS)
                                c = Color.Red;
                            else
                                c = Color.Black;


                            int alphaChange = 200;
                            double maxAgeFade = 30;
                            double negativeAge = -5;

                            if (selectedTarget >= 0 && t == targets[selectedTarget])
                            {
                                c = Color.Blue;
                                //maxAgeFade = 60;
                            }


                            Blip last = null;
                            for (int i = 0; i < t.Blips.Count; i++)
                            {
                                Blip b = t.Blips[i];
                                last = b;
                                double age = currentTime - b.time;
                                //using (Brush br = new SolidBrush(Color.FromArgb(255 - alphaChange * (t.Blips.Count - 1 - i) / toShow, c)))
                                if (age < maxAgeFade && age >= negativeAge)
                                {
                                    if (age < 0)
                                    {
                                        using (Brush br = new SolidBrush(Color.FromArgb(255 - (int)(alphaChange * age / negativeAge), Color.Green)))
                                        {
                                            g.FillEllipse(br, (float)b.X - blipRadius, (float)b.Y - blipRadius, 2 * blipRadius, 2 * blipRadius);
                                        }
                                    }
                                    else
                                    {
                                        using (Brush br = new SolidBrush(Color.FromArgb(255 - (int)(alphaChange * age / maxAgeFade), c)))
                                        {
                                            g.FillEllipse(br, (float)b.X - blipRadius, (float)b.Y - blipRadius, 2 * blipRadius, 2 * blipRadius);
                                        }
                                    }
                                }
                            }

                            if (last != null)
                            {
                                double age = currentTime - last.time;
                                if (age < 20 && age > -5)
                                {
                                    g.DrawString(t.targetID.ToString(), SystemFonts.DefaultFont, Brushes.Black, (float)(last.X + blipRadius * 1.5), (float)(last.Y + blipRadius * 1.5));
                                    using (Pen p = new Pen(Color.Orange, 4))
                                    {
                                        PointD est = t.EstimatedPos(currentTime + 1);
                                        g.DrawLine(p, (float)last.X, (float)last.Y, (float)est.X, (float)est.Y);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex.Message);
                    }

                }
            }
            #endregion
        }

        bool paused = false;
        private void btnPause_Click(object sender, EventArgs e)
        {
            paused = !paused;
        }

        bool autoUpdate = false;
        int selectedTarget = -1;
        private void lvTargets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (autoUpdate)
                return;

            
            if (lvTargets.SelectedIndices.Count > 0)
                selectedTarget = int.Parse(lvTargets.Items[lvTargets.SelectedIndices[0]].SubItems[0].Text);
            else
                selectedTarget = -1;

            if (loaded && selectedTarget >= 0)
            {
                currentTime = targets[selectedTarget].lastBlip.time;
                tbTime.Value = (int)(currentTime);
            }

            if (selectedTarget >= 0)
            {
                myDisplay1.Invalidate();
                Application.DoEvents();
            }
        }

        bool step = false;
        private void btnStep_Click(object sender, EventArgs e)
        {
            step = true;
        }
    }

    public class MyDisplay : Panel
    {
        public MyDisplay()
        {
            this.DoubleBuffered = true;
        }
    }
}
