namespace AI_Assistant
{
    public partial class frmPromptSettings : Form
    {
        private PromptSettings _settings = null!;
        private PromptEntry? _selected; // the entry currently shown in the editor

        public frmPromptSettings()
        {
            InitializeComponent();
            LoadPrompts(PromptStore.Settings.Clone());
        }

        private void LoadPrompts(PromptSettings settings)
        {
            _selected = null;
            _settings = settings;

            lstPrompts.Items.Clear();
            foreach (string key in _settings.Prompts.Keys)
            {
                lstPrompts.Items.Add(key);
            }
            txtBasePrompt.Text = _settings.BaseSystemPrompt;
            lstPrompts.SelectedIndex = 0;
        }

        // Writes the text boxes back into the entry that was being edited.
        private void CommitSelected()
        {
            if (_selected != null)
            {
                _selected.Instruction = txtInstruction.Text;
                _selected.UserTemplate = txtTemplate.Text;
            }
        }

        private void lstPrompts_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommitSelected();

            _selected = lstPrompts.SelectedItem is string key ? _settings.Prompts[key] : null;
            lblDescription.Text = _selected?.Description ?? string.Empty;
            txtInstruction.Text = _selected?.Instruction ?? string.Empty;
            txtTemplate.Text = _selected?.UserTemplate ?? string.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CommitSelected();
            _settings.BaseSystemPrompt = txtBasePrompt.Text;

            try
            {
                PromptStore.Save(_settings);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not save the prompts: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Reset all prompts to their defaults? (Nothing is saved until you press Save.)", "Reset Prompts", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LoadPrompts(PromptSettings.CreateDefault());
            }
        }
    }
}
