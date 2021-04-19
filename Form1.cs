using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Managing.ARFF.Files
{
    public partial class Form1 : Form
    {
        private List<string> listOfAllAttributeNames;
        private List<string> dataReadyToWrite;
        private List<string> attributesReadyToWrite;
        private string relation;

        public Form1()
        {
            InitializeComponent();
            listOfAllAttributeNames = new List<string>();
            catalogPathTextBox.Text = "C:\\Users\\valde\\Desktop\\arff-test";
        }

        private void chooseCatalogButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                catalogPathTextBox.Text = dialog.SelectedPath;
                listOfAllAttributeNames = new List<string>();
                dataReadyToWrite = new List<string>();
                attributesReadyToWrite = new List<string>();
                richTextBox1.Text = null;
                checkedListBoxAll.Items.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            try
            {
                richTextBox1.Text = null;
                dataReadyToWrite = new List<string>();
                attributesReadyToWrite = new List<string>();
                listOfAllAttributeNames = new List<string>();
                checkedListBoxAll.Items.Clear();
                getAllAttributes(catalogPathTextBox.Text);
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            button1.Enabled = true;
        }

        private void getAllAttributes(string folderPath)
        {
            relation = null;
            string line;
            bool fileIsUsable = false;
            string[] files = Directory.GetFiles(folderPath, "*.arff", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                try
                {
                    if (Path.GetExtension(file) == ".arff")
                    {
                        StreamReader ongoingFile = new StreamReader(file);
                        if(relation == null)
                            relation = ongoingFile.ReadLine();
                        if (relation.Contains("@relation"))
                        {
                            while ((line = ongoingFile.ReadLine()) != null)
                            {
                                if (line.Contains("@attribute") == true)
                                {
                                    listOfAllAttributeNames.Add(line);
                                }
                                if (line.Contains("@data") == true)
                                {
                                    fileIsUsable = true;
                                }
                            }
                            if (fileIsUsable == true)
                            {
                                foreach (string attr in listOfAllAttributeNames)
                                {
                                    checkedListBoxAll.Items.Add(attr);
                                }
                                Console.WriteLine($"Found attributes: {listOfAllAttributeNames.Count}");
                                //Console.WriteLine($"Found [.arff] files: {files.Length}");
                                ongoingFile.Close();
                                break;
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
                catch(Exception exc)
                {
                    relation = null;
                    listOfAllAttributeNames.Clear();
                    Console.WriteLine(exc.Message);
                }
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

        private void combineButton_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            chooseCatalogButton.Enabled = false;
            button2.Enabled = false;
            checkedListBoxAll.Enabled = false;
            if (checkedListBoxAll.Items.Count != 0)
            {
                if(checkedListBoxAll.CheckedItems.Count != 0)
                {
                    try
                    {
                        dataReadyToWrite = new List<string>();
                        attributesReadyToWrite = new List<string>();
                        richTextBox1.Text = null;
                        combineFunc(getListToCombine(), catalogPathTextBox.Text);
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
            checkedListBoxAll.Enabled = true;
            button2.Enabled = true;
            button1.Enabled = true;
            chooseCatalogButton.Enabled = true;
        }

        private List<string> setCheckedItemsToList()
        {
            List<string> temp = new List<string>(); 
            foreach (var attr in checkedListBoxAll.CheckedItems)
            {
                temp.Add(attr.ToString());
            }
            return temp;
        }

        private void combineFunc(List<string> toCombine, string folderPath)
        {
            attributesReadyToWrite = setCheckedItemsToList();
            string allData;
            string[] seperatedData = null;
            string line;
            bool fileIsUsable = false;
            chooseCatalogButton.Enabled = false;
            combineButton.Enabled = false;
            foreach(string file in Directory.GetFiles(folderPath, "*.arff", SearchOption.TopDirectoryOnly))
            {
                try
                {
                    if (Path.GetExtension(file) == ".arff")
                    {
                        StreamReader ongoingFile = new StreamReader(file);
                        if (ongoingFile.ReadLine() == relation)
                        {
                            while ((line = ongoingFile.ReadLine()) != null)
                            {
                                if (line.Contains("@data") == true)
                                {
                                    ongoingFile.ReadLine();
                                    allData = ongoingFile.ReadLine();
                                    seperatedData = allData.Split(',');
                                    if (allData != "" && !string.IsNullOrEmpty(allData) && seperatedData.Length == listOfAllAttributeNames.Count)
                                    {
                                        fileIsUsable = true;
                                    }
                                }
                            }
                            if(fileIsUsable == true)
                            {
                                string oneFileData = null;
                                foreach (string attr in listOfAllAttributeNames)
                                {
                                    foreach (string temp in attributesReadyToWrite)
                                    {
                                        if (temp == attr)
                                        {
                                            if (oneFileData != null)
                                                oneFileData += ("," + seperatedData[listOfAllAttributeNames.IndexOf(attr)]);
                                            else
                                                oneFileData += seperatedData[listOfAllAttributeNames.IndexOf(attr)];
                                        }
                                    }
                                }
                                dataReadyToWrite.Add(oneFileData);
                                richTextBox1.Text += $"{oneFileData}\n";
                                //Console.WriteLine(oneFileData);
                            }
                            else
                            {
                                throw new Exception($"File skipped [{Path.GetFileName(file)}]: \"@data\" tag not found or data is corrupted!");
                            }
                        }
                        else
                        {
                            throw new Exception($"File skipped [{Path.GetFileName(file)}]: \"@realation\" tag not found!");
                        }
                    }
                    else
                    {
                        throw new Exception($"File skipped [{Path.GetFileName(file)}]: extention is not \".arff\"!");
                    }
                }
                catch(Exception exc)
                {
                    fileIsUsable = false;
                    Console.WriteLine(exc.Message);
                }
            }
            chooseCatalogButton.Enabled = true;
            combineButton.Enabled = true;
        }

        private List<string> getListToCombine()
        {
            List<string> final = new List<string>();
            foreach(var attr in checkedListBoxAll.CheckedItems)
            {
                final.Add(attr.ToString());
            }
            return final;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataReadyToWrite.Count != 0 && attributesReadyToWrite.Count != 0)
            {
                List<string> lines = new List<string>();
                lines.Add($"{relation}\n");
                foreach(string attr in attributesReadyToWrite)
                {
                    lines.Add(attr);
                }
                lines.Add($"\n@data\n");
                foreach(string data in dataReadyToWrite)
                {
                    lines.Add(data);
                }                string docPath = "C:\\Users\\valde\\Desktop";
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "combined.arff")))
                {
                    foreach (string line in lines)
                        outputFile.WriteLine(line);
                }
                MessageBox.Show("Data succesfully saved to file!");
            }
            else
            {
                MessageBox.Show("No data to write!");
            }
        }
    }
}
