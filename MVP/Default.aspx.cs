using MVP.Controllers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Newtonsoft.Json;
using System.Data;

namespace MVP
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Inicializa o DataTable
                DataTable dt = new DataTable();
                dt.Columns.Add("Descricao");
                dt.Columns.Add("Data");

                // Armazena o DataTable no ViewState
                ViewState["ItensDataTable"] = dt;

                // Define o DataTable como a fonte de dados do GridView
                grvDados.DataSource = dt;
                grvDados.DataBind();
            }
        }

        protected async void cldData_SelectionChanged(object sender, EventArgs e)
        {
            // Sua lógica aqui
            DateTime dataSelecionada = cldData.SelectedDate;
            await VerificaFeriadoAsync(dataSelecionada.ToShortDateString());
            //ExibirMensagem();
        }

        public async Task VerificaFeriadoAsync(String data)
        {
            try
            {
                // Criar uma instância do controlador
                var defaultController = new DefaultController();

                // Chamar a função do controlador e obter o resultado
                var resultado = await defaultController.ValidarDataComoFeriado(DateTime.ParseExact(data, "dd/MM/yyyy", null), "RJ");

                // Verificar se o resultado é um JsonResult
                if (resultado is JsonResult jsonResult)
                {
                    try
                    {
                        string jsonContent = JsonConvert.SerializeObject(jsonResult.Data);

                        // Desserializar o JSON para um objeto RespostaJson
                        var respostaJson = JsonConvert.DeserializeObject<RespostaJson>(jsonContent);

                        // Agora, a variável respostaJson conterá os valores desserializados
                        bool ehFeriado = respostaJson.EhFeriado;

                        // Usar o valor conforme necessário
                        if (ehFeriado)
                        {
                            lblAlertData.Visible = true;
                            lblAlertData.Text = string.Concat(respostaJson.NivelFeriado != "nacional" ? $" <span style='color: red;'><b>{respostaJson.NivelFeriado.ToUpper()}</b></span> - " : "", respostaJson.NomeFeriado, respostaJson.TipoFeriado != "feriado" ? $" (<span style='color: red;'><u>{respostaJson.TipoFeriado}</u></span>)" : "");


                        }
                        else
                        {
                            lblAlertData.Visible = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        // Se ocorrer uma exceção ao tentar converter, registrar detalhes
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "MyKey", $"alert('Erro ao converter EhFeriado: {ex.Message}');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                // Se ocorrer uma exceção, tratar conforme necessário
                Page.ClientScript.RegisterStartupScript(this.GetType(), "MyKey", $"alert('Erro: {ex.Message}');", true);
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            // Atualiza o GridView
            grvDados.DataSource = null;
            grvDados.DataBind();

            btnLimpar.Visible = false;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            var descricao = tbDescricao.Text;
            var data = cldData.SelectedDate.ToShortDateString();

            // Obtém o DataTable do ViewState
            DataTable dt = (DataTable)ViewState["ItensDataTable"];

            // Adiciona uma nova linha ao DataTable
            DataRow row = dt.NewRow();
            row["Descricao"] = descricao;
            row["Data"] = data;
            dt.Rows.Add(row);

            // Atualiza o GridView
            grvDados.DataSource = dt;
            grvDados.DataBind();

            btnLimpar.Visible = true;

            // Limpa os campos após adicionar à lista
            tbDescricao.Text = string.Empty;
            cldData.SelectedDate = DateTime.MinValue;
            lblAlertData.Visible = false;
        }

    }

    public class RespostaJson
    {
        public bool EhFeriado { get; set; }
        public string NomeFeriado { get; set; }
        public string TipoFeriado { get; set; }
        public string NivelFeriado { get; set; }

    }
}