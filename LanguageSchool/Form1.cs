using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LanguageSchool;

public partial class Form1 : Form
{
    private static readonly HttpClient client = new() { BaseAddress = new Uri("http://localhost:5248/") };
    private CancellationTokenSource? _cts;

    public Form1()
    {
        InitializeComponent();
        Help();
    }
    private void Help()
    {
        numericUpDown1.DecimalPlaces = 1;
        numericUpDown1.Increment = 0.1m;
        toolTip1.SetToolTip(loadButton, "Получить с сервера актуальный список групп");
        toolTip1.SetToolTip(cancelButton, "Прервать текущую операцию загрузки");
        toolTip1.SetToolTip(syncLoadButton, "Учебный эксперимент: блокировка интерфейса");
        toolTip1.SetToolTip(addButton, "Сохранить новую группу на сервере");
        toolTip1.SetToolTip(btnDelete, "Удалить выбранную группу из системы");
        toolTip1.SetToolTip(textBox1, "Название изучаемого языка, от 2 символов");
        toolTip1.SetToolTip(textBox3, "ФИО преподавателя группы");
        toolTip1.SetToolTip(numericUpDown1, "Средний рейтинг группы (от 1.0 до 5.0)");
        toolTip1.SetToolTip(textBox4, "Ссылка на чат или материалы группы");
        textBox1.PlaceholderText = "Например: Английский";
        textBox3.PlaceholderText = "Например: Иван Иванов";
        textBox4.PlaceholderText = "Например: https://t.me";
        helpProvider1.SetHelpString(textBox1, "Название изучаемого иностранного языка. От 2 до 100 символов. Например: Английский.");
        helpProvider1.SetHelpString(textBox3, "ФИО преподавателя, ведущего занятия в этой группе.");
        helpProvider1.SetHelpString(numericUpDown1, "Средний балл успеваемости группы по шкале от 1.0 до 5.0.");
        helpProvider1.SetHelpString(textBox4, "Электронная ссылка на чат группы или учебные материалы.");
        helpProvider1.SetHelpString(equipmentGrid, "Список учебных групп на сервере. Для обновления нажмите кнопку «Загрузить». Подробнее — в меню «Справка».");
    }
    private void ShowErrors(ValidationProblem? problem)
    {
        if (problem?.Errors is null)
            return;

        var fields = new Dictionary<string, Control>(StringComparer.OrdinalIgnoreCase)
        {
            ["Language"] = textBox1,
            ["Teacher"] = textBox3,
            ["Rating"] = numericUpDown1,
            ["Link"] = textBox4
        };

        foreach (var (field, messages) in problem.Errors)
        {
            if (fields.TryGetValue(field, out Control? control))
            {
                errorProvider1.SetError(control, string.Join(" ", messages));
            }
        }
    }

    private static string FormatErrors(ValidationProblem? problem)
    {
        if (problem?.Errors is null || problem.Errors.Count == 0)
            return "Данные не прошли проверку на сервере.";

        IEnumerable<string> messages = problem.Errors.SelectMany(pair => pair.Value);
        return "Исправьте ошибки (поля отмечены красным значком):" +
            Environment.NewLine + string.Join(Environment.NewLine, messages);
    }
    private async void loadButton_Click_1(object sender, EventArgs e)
    {
        progressBar.Visible = true;
        statusLabel.Text = "Загрузка...";
        _cts = new CancellationTokenSource();

        try
        {
            var groups = await client.GetFromJsonAsync<List<Group>>("api/groups", _cts.Token);
            equipmentGrid.DataSource = groups;
            equipmentGrid.Columns["Language"].HeaderText = "Язык";
            equipmentGrid.Columns["Teacher"].HeaderText = "Преподаватель";
            equipmentGrid.Columns["Rating"].HeaderText = "Рейтинг";
            equipmentGrid.Columns["Link"].HeaderText = "Ссылка";
            statusLabel.Text = $"Загружено записей: {groups?.Count ?? 0}";
        }
        catch (OperationCanceledException)
        {
            statusLabel.Text = "Загрузка отменена";
        }
        catch (Exception ex)
        {
            statusLabel.Text = "Ошибка соединения";
            MessageBox.Show("Нет связи с сервером:\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            progressBar.Visible = false;
        }
    }
    private void cancelButton_Click_1(object sender, EventArgs e)
    {
        _cts?.Cancel();
        MessageBox.Show("Загрузка отменена пользователем.", "Отмена операции", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    private void experiment_Click_1(object sender, EventArgs e)
    {
        statusLabel.Text = "Эксперимент (блокировка)...";
        progressBar.Visible = true;
        try
        {
            var task = client.GetFromJsonAsync<List<Group>>("api/groups");
            task.Wait();
            equipmentGrid.DataSource = task.Result;
            statusLabel.Text = "Готово (синхронно)";
        }
        catch (Exception ex)
        {
            statusLabel.Text = "Ошибка эксперимента";
            MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            progressBar.Visible = false;
        }
    }
    private async void btnDelete_Click(object sender, EventArgs e)
    {
        if (equipmentGrid.SelectedRows.Count == 0)
        {
            MessageBox.Show("Выберите группу в таблице для удаления.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var selectedGroup = (Group)equipmentGrid.SelectedRows[0].DataBoundItem;

        try
        {
            HttpResponseMessage response = await client.DeleteAsync($"api/groups/{selectedGroup.Id}");
            if (response.IsSuccessStatusCode)
            {
                statusLabel.Text = "Группа удалена";
                MessageBox.Show("Группа успешно удалена из системы!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadButton_Click_1(sender, e);
            }
            else
            {
                MessageBox.Show("Не удалось удалить группу с сервера.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Нет связи с сервером:\n" + ex.Message, "Ошибка соединения", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private async void addButton_Click_1(object sender, EventArgs e)
    {
        errorProvider1.Clear();

        if (string.IsNullOrWhiteSpace(textBox1.Text))
        {
            errorProvider1.SetError(textBox1, "Укажите название языка");
            textBox1.Focus();
            return;
        }

        var request = new
        {
            Language = textBox1.Text,
            Teacher = textBox3.Text,
            Rating = numericUpDown1.Value,
            Link = string.IsNullOrWhiteSpace(textBox4.Text) ? null : textBox4.Text
        };

        try
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/groups", request);
            if (response.IsSuccessStatusCode)
            {
                statusLabel.Text = "Группа добавлена";
                MessageBox.Show("Новая учебная группа успешно добавлена в систему!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadButton_Click_1(sender, e);
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                statusLabel.Text = "Данные не прошли проверку";
                ValidationProblem? problem = await response.Content.ReadFromJsonAsync<ValidationProblem>();
                ShowErrors(problem);
                MessageBox.Show(FormatErrors(problem), "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (response.StatusCode == HttpStatusCode.Conflict)
            {
                statusLabel.Text = "Группа уже существует";
                MessageBox.Show("Группа по такому языку с этим преподавателем уже существует.", "Конфликт", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Произошла ошибка при отправке данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Нет связи с сервером:\n" + ex.Message, "Ошибка соединения", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void userGuide_Click(object sender, EventArgs e)
    {
        string path = Path.Combine(AppContext.BaseDirectory, "Help", "index.html");
        if (!File.Exists(path))
        {
            MessageBox.Show("Файл справки не найден:\n" + path, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
    }
    private void about_Click(object sender, EventArgs e)
    {
        MessageBox.Show(
            "Центр языкового обучения — управление группами" + Environment.NewLine +
            $"Версия {Application.ProductVersion}" + Environment.NewLine +
            "Курсовой проект по МДК 02.01" + Environment.NewLine +
            "Автор: Чернобай А.А., группа 24-ИСП-1",
            "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void label4_Click(object sender, EventArgs e)
    {

    }
}