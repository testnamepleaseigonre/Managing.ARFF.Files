using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Managing.ARFF.Files
{
    public partial class Form1 : Form
    {
        private List<string> listOfAllAttributeNames;
        private List<string> dataReadyToWrite;
        private List<string> attributesReadyToWrite;
        private ManualResetEvent mre = new ManualResetEvent(true);
        private List<string> reliableFilesInDirectory;
        private CancellationTokenSource cts;
        private string relation;
        public Form1()
        {
            InitializeComponent();
            mre = new ManualResetEvent(true);
            dataReadyToWrite = new List<string>();
            attributesReadyToWrite =new List<string>();
            listOfAllAttributeNames = new List<string>();
            combineButton.Enabled = false;
            checkedListBoxAll.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            pauseButton.Enabled = false;
            continueButton.Enabled = false;
            stopButton.Enabled = false;
            // --HELPERS
            catalogPathTextBox.Text = "C:\\Users\\valde\\Desktop\\arff-test";
            setReliableFiles(catalogPathTextBox.Text);
            // --HELPERS
            progressBar.Minimum = 0;
            progressBar.Value = 0;
            progressBar.Step = 1;
        }

        delegate void Runnable();

        private void chooseCatalogButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                catalogPathTextBox.Text = dialog.SelectedPath;
                listOfAllAttributeNames = new List<string>();
                dataReadyToWrite = new List<string>();
                attributesReadyToWrite = new List<string>();
                reliableFilesInDirectory = new List<string>();
                checkedListBoxAll.Items.Clear();
                setReliableFiles(dialog.SelectedPath);
            }
        }

        private void setReliableFiles(string folderPath)
        {
            Thread th = new Thread(() =>
            {
                unavailableToClose(true);
                blockUnblockButtonsForCombine(false);
                reliableFilesInDirectory = new List<string>();
                int attrCount = 0;
                int dataCount = 0;
                string allData;
                string line;
                bool fileHasDataTag = false;
                Invoke((Action)delegate
                {
                    progressBar.Value = 0;
                    progressBar.Maximum = Directory.GetFiles(folderPath, "*.arff", SearchOption.TopDirectoryOnly).Length;
                });
                foreach (string file in Directory.GetFiles(folderPath, "*.arff", SearchOption.TopDirectoryOnly))
                {
                    Invoke((Action)delegate
                    {
                        progressBar.PerformStep();
                    });
                    try
                    {
                        if (Path.GetExtension(file) == ".arff")
                        {
                            StreamReader ongoingFile = new StreamReader(file);
                            if (ongoingFile.ReadLine().Contains("@relation"))
                            {
                                while ((line = ongoingFile.ReadLine()) != null)
                                {
                                    if (line.Contains("@attribute") == true)
                                    {
                                        attrCount++;
                                    }
                                    if (line.Contains("@data") == true)
                                    {
                                        fileHasDataTag = true;
                                        ongoingFile.ReadLine();
                                        allData = ongoingFile.ReadLine();
                                        dataCount = allData.Split(',').Length;
                                        ongoingFile.Close();
                                        break;
                                    }
                                }
                                if (fileHasDataTag == true)
                                {
                                    if (dataCount == attrCount)
                                    {
                                        reliableFilesInDirectory.Add(file);
                                        //Console.WriteLine(file);
                                    }
                                    else
                                    {
                                        throw new Exception($"File skipped [{Path.GetFileName(file)}]: data is corrupted!");
                                    }
                                }
                                else
                                {
                                    throw new Exception($"File skipped [{Path.GetFileName(file)}]: \"@data\" tag not found!");
                                }
                            }
                            else
                            {
                                throw new Exception($"File skipped[{Path.GetFileName(file)}]: \"@realation\" tag not found!");
                            }
                        }
                        else
                        {
                            throw new Exception($"File skipped[{Path.GetFileName(file)}]: extention is not \".arff\"!");
                        }
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                    }
                    attrCount = 0;
                    fileHasDataTag = false;
                }
                blockUnblockButtonsForCombine(true);
                unavailableToClose(false);
            });
            th.Name = "SetReliableFiles thread";
            th.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Thread th1 = new Thread(() => getAllAttributes(catalogPathTextBox.Text));
                th1.Name = "Attribute getting thread";
                th1.Start();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void getAllAttributes(string folderPath)
        {
            Thread th = new Thread(() =>
            {
                unavailableToClose(true);
                blockUnblockButtonsForCombine(false);
                string line;
                dataReadyToWrite = new List<string>();
                attributesReadyToWrite = new List<string>();
                listOfAllAttributeNames = new List<string>();
                Invoke((Action)delegate 
                {
                    checkedListBoxAll.Items.Clear(); 
                });
                StreamReader ongoingFile = new StreamReader(reliableFilesInDirectory[0]);
                relation = ongoingFile.ReadLine();
                while ((line = ongoingFile.ReadLine()) != null)
                {
                    if (line.Contains("@attribute") == true)
                    {
                        listOfAllAttributeNames.Add(line);
                    }
                    else if(line.Contains("@data"))
                    {
                        break;
                    }
                }
                moveAttributesToListBox();
                blockUnblockButtonsForCombine(true);
                unavailableToClose(false);
            });
            th.Name = "GetAttributes thread";
            th.Start();
        }

        private void moveAttributesToListBox()
        {
            foreach (string attr in listOfAllAttributeNames)
            {
                Invoke((Action)delegate
                {
                    checkedListBoxAll.Items.Add(attr);
                });
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            setAllOrNoneChecked(true);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            setAllOrNoneChecked(false);
        }

        private void setAllOrNoneChecked(bool choice)
        {
            if (checkedListBoxAll.Items.Count != 0)
            {
                for (var i = 0; i < checkedListBoxAll.Items.Count; i++)
                {
                    checkedListBoxAll.SetItemChecked(i, choice);
                }
            }
            else
            {
                if(choice == true)
                    MessageBox.Show("Nothing to check!");
                else
                    MessageBox.Show("Nothing to uncheck!");
            }
        }

        private List<string> setCheckedItemsToList()
        {
            List<string> temp = new List<string>();
            foreach (string attr in checkedListBoxAll.CheckedItems)
            {
                temp.Add(attr);
            }
            return temp;
        }

        private void combineButton_Click(object sender, EventArgs e)
        {
            if (checkedListBoxAll.Items.Count != 0)
            {
                if(checkedListBoxAll.CheckedItems.Count != 0)
                {
                    try
                    {
                        Invoke((Action)delegate
                        {
                            progressBar.Value = 0;
                            progressBar.Maximum = reliableFilesInDirectory.Count;
                        });
                        cts = new CancellationTokenSource();
                        CancellationToken ct = cts.Token;
                        Thread th1 = new Thread(() => combineFunc(ct));
                        th1.Name = "Combining thread";
                        th1.Start();
                    }
                    catch(Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Check at least one item to combine!");
                }
            }
            else
            {
                MessageBox.Show("Nothing to combine!");
            }
        }

        private void enableStopButtons(bool choice)
        {
            if (choice == true)
            {
                Invoke((Action)delegate
                {
                    pauseButton.Enabled = true;
                    continueButton.Enabled = true;
                    stopButton.Enabled = true;

                });
            }
            else
            {
                Invoke((Action)delegate
                {
                    pauseButton.Enabled = false;
                    continueButton.Enabled = false;
                    stopButton.Enabled = false;
                });
            }
        }

        private void unavailableToClose(bool choice)
        {
            if(choice == true)
            {
                Invoke((Action)delegate
                {
                    this.FormBorderStyle = FormBorderStyle.None;
                });
            }
            else
            {
                Invoke((Action)delegate
                {
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                });

            }
        }

        private void combineFunc(CancellationToken ct)
        {
            unavailableToClose(true);
            blockUnblockButtonsForCombine(false);
            enableStopButtons(true);
            dataReadyToWrite = new List<string>();
            attributesReadyToWrite = setCheckedItemsToList();
            List<int> indexes = new List<int>();
            foreach (string temp in attributesReadyToWrite)
            {
                foreach (string attr in listOfAllAttributeNames)
                {
                    if (attr == temp)
                    {
                        indexes.Add(listOfAllAttributeNames.IndexOf(attr));
                    }
                }
            }
            foreach (string file in reliableFilesInDirectory)
            {
                mre.WaitOne();
                if (ct.IsCancellationRequested)
                {
                    return;
                }
                string[] seperatedData = File.ReadAllLines(file).Last().Split(',');
                StringBuilder sb = new StringBuilder();
                foreach (int index in indexes)
                {
                    if (sb.Length != 0)
                    {
                        sb.Append($",{seperatedData[index]}");
                    }
                    else
                    {
                        sb.Append(seperatedData[index]);
                    }
                }
                dataReadyToWrite.Add(sb.ToString());
                Invoke((Action)delegate
                {
                    progressBar.PerformStep();
                });
            }
            enableStopButtons(false);
            blockUnblockButtonsForCombine(true);
            unavailableToClose(false);
        }

        private void blockUnblockButtonsForCombine(bool choice)
        {
            if(choice == true)
            {
                Invoke((Action)delegate
                {
                    chooseCatalogButton.Enabled = true;
                    combineButton.Enabled = true;
                    checkedListBoxAll.Enabled = true;
                    button1.Enabled = true;
                    button2.Enabled = true;
                });
            }
            else
            {
                Invoke((Action)delegate
                {
                    chooseCatalogButton.Enabled = false;
                    combineButton.Enabled = false;
                    checkedListBoxAll.Enabled = false;
                    button1.Enabled = false;
                    button2.Enabled = false;
                });
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataReadyToWrite.Count != 0 && attributesReadyToWrite.Count != 0 && attributesReadyToWrite != null)
            {
                writeToFile();
            }
            else
            {
                MessageBox.Show("No data to write!");
            }
        }

        private void writeToFile()
        {
            Thread th = new Thread(() =>
            {
                blockUnblockButtonsForCombine(false);
                List<string> lines = new List<string>();
                lines.Add($"{relation}\n");
                foreach (string attr in attributesReadyToWrite)
                {
                    lines.Add(attr);
                }
                lines.Add($"\n@data\n");
                foreach (string data in dataReadyToWrite)
                {
                    lines.Add(data);
                }
                string docPath = "C:\\Users\\valde\\Desktop";
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "combined.arff")))
                {
                    foreach (string line in lines)
                        outputFile.WriteLine(line);
                }
                MessageBox.Show("Data succesfully saved to file!");
                blockUnblockButtonsForCombine(true);
            });
            th.Name = "Writing thread";
            th.Start();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            mre.Reset();
            DialogResult result = MessageBox.Show($"Do you really want to abort this action?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                cts.Cancel();
                dataReadyToWrite = new List<string>();
                attributesReadyToWrite = new List<string>();
                blockUnblockButtonsForCombine(true);
                enableStopButtons(false);
                mre.Set();
                progressBar.Value = progressBar.Maximum;
                unavailableToClose(false);
            }
            else
            {
                mre.Set();
            }
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            mre.Reset();
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            mre.Set();
        }
    }
}
