﻿using System.Windows.Forms;
using BrainfuckDebugger.Interfaces;
using System;
using Brainfuck;

namespace BrainfuckDebugger
{
    public partial class MainForm : Form, IMainView
    {
        private MainViewPresenter presenter;
        private Int32 nextInputIndex;

        /// <summary>
        /// The string of the Brainfuck program
        /// </summary>
        public string BrainfuckProgram 
        {
            get { return programTextBox.Text; }
            set { programTextBox.Text = value; }
        }

        private string ProgramOutput
        {
            get { return outputTextBox.Text; }
            set { outputTextBox.Text = value; }
        }

        public MainForm()
        {
            InitializeComponent();

            presenter = new MainViewPresenter(this);
            nextInputIndex = 0;
        }

        /// <summary>
        /// Asks the user to choose a file through an OpenFileDialog
        /// </summary>
        /// <param name="action">The action we want to do with the file</param>
        /// <returns>The file name chosen by the user</returns>
        public string ChooseFile(FileAction action)
        {
            FileDialog fileDialog;

            switch (action)
            {
                case FileAction.Save:
                    fileDialog = new SaveFileDialog();
                    break;

                case FileAction.Open:
                    fileDialog = new OpenFileDialog();
                    break;

                default:
                    throw new ArgumentException("The argument should be a valid FileAction enum value.", "action");
            }

            fileDialog.ShowDialog();
            
            return fileDialog.FileName;
        }

        /// <summary>
        /// Gets input for the brainfuck program
        /// </summary>
        /// <returns></returns>
        public string GetInput()
        {
            if (promptForInputRadioButton.Checked)
            {
                using (var inputForm = new InputForm())
                {
                    inputForm.ShowDialog();
                    return inputForm.Value;
                }
            }
            else
            {
                var inputValues = inputTextBox.Text.Split(';');

                if (nextInputIndex >= inputValues.Length)
                    throw new Exception("You did not supply enough arguments");

                return inputValues[nextInputIndex++];
            }
        }

        /// <summary>
        /// Clears the program output
        /// </summary>
        public void ClearOutput()
        {
            ProgramOutput = "";
        }

        /// <summary>
        /// Writes the output of the program
        /// </summary>
        /// <param name="value"></param>
        public void WriteToOutput(string value)
        {
            if (ProgramOutput != "")
                ProgramOutput += " ";

            ProgramOutput += value;
        }

        private void loadFromFileButton_Click(object sender, System.EventArgs e)
        {
            presenter.LoadFromFile();
        }

        private void executeButton_Click(object sender, System.EventArgs e)
        {
            nextInputIndex = 0;
            presenter.ExecuteProgram();
        }

        private void saveToFileButton_Click(object sender, System.EventArgs e)
        {
            presenter.SaveToFile();
        }

        private void preSupplyInputRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            inputTextBox.Enabled = preSupplyInputRadioButton.Checked;
        }
    }
}
