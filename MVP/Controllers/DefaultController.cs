using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Globalization;

namespace MVP.Controllers
{

    public class DefaultController : Controller
    {
        public static object feriados;

        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> ValidarDataComoFeriado(DateTime data, String uf)
        {
            try
            {
                if (DefaultController.feriados != null && DefaultController.feriados is List<Feriado> listaFeriados && listaFeriados.Any())
                {
                    if (DateTime.Parse(listaFeriados[0].Date).Year == data.Year)
                    {
                        var feriados1 = (List<Feriado>)DefaultController.feriados;

                        // Verificar se a data está presente na lista de feriados
                        var feriadoEncontrado = feriados1.Any(f => f.Date == data.ToString("yyyy-MM-dd"));

                        if (feriadoEncontrado)
                        {
                            // Agora, você pode verificar o tipo de feriado (feriado ou ponto facultativo) no objeto Feriado
                            var nomeFeriado = feriados1.FirstOrDefault(f => f.Date == data.ToString("yyyy-MM-dd"))?.Name;
                            var tipoFeriado = feriados1.FirstOrDefault(f => f.Date == data.ToString("yyyy-MM-dd"))?.Type;
                            var nivelFeriado = feriados1.FirstOrDefault(f => f.Date == data.ToString("yyyy-MM-dd"))?.Level;

                            // Retornar a resposta para a página ou fazer algo com ela
                            return Json(new
                            {
                                EhFeriado = true,
                                NomeFeriado = nomeFeriado,
                                TipoFeriado = tipoFeriado,
                                NivelFeriado = nivelFeriado
                            });
                        }
                        else
                        {
                            return Json(new { EhFeriado = false });
                        }
                    }
                    else
                    {
                        return await preencheFeriadosAsync(data, uf);
                    }
                }
                else
                {
                    return await preencheFeriadosAsync(data, uf);
                }
            }
            catch (Exception ex)
            {
                // Se ocorrer uma exceção, tratar conforme necessário
                return Json(new { Erro = ex.Message });
            }
        }

        public async Task<JsonResult> preencheFeriadosAsync(DateTime data, String uf)
        {
            // Construir a URL da API com os parâmetros necessários, incluindo o token
            string apiUrl = $"https://api.invertexto.com/v1/holidays/{data.Year}?state={uf}&prefix=5828&token=PJpmDxLsDPxAVxqLdHNy76J3c3pKShY8";

            // Configurar o HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Definir o timeout em 30 segundos
                client.Timeout = TimeSpan.FromSeconds(30);

                // Fazer a chamada GET para a API
                HttpResponseMessage resposta = await client.GetAsync(apiUrl);

                // Verificar se a chamada foi bem-sucedida
                if (resposta.IsSuccessStatusCode)
                {
                    // Ler o conteúdo da resposta
                    string resultadoJson = await resposta.Content.ReadAsStringAsync();

                    // Desserializar o JSON para uma lista de feriados
                    DefaultController.feriados = JsonConvert.DeserializeObject<List<Feriado>>(resultadoJson);
                    var feriados1 = (List<Feriado>)DefaultController.feriados;

                    // Verificar se a data está presente na lista de feriados
                    var feriadoEncontrado = feriados1.Any(f => f.Date == data.ToString("yyyy-MM-dd"));

                    if (feriadoEncontrado)
                    {
                        // Agora, você pode verificar o tipo de feriado (feriado ou ponto facultativo) no objeto Feriado
                        var nomeFeriado = feriados1.FirstOrDefault(f => f.Date == data.ToString("yyyy-MM-dd"))?.Name;
                        var tipoFeriado = feriados1.FirstOrDefault(f => f.Date == data.ToString("yyyy-MM-dd"))?.Type;
                        var nivelFeriado = feriados1.FirstOrDefault(f => f.Date == data.ToString("yyyy-MM-dd"))?.Level;

                        // Retornar a resposta para a página ou fazer algo com ela
                        return Json(new
                        {
                            EhFeriado = true,
                            NomeFeriado = nomeFeriado,
                            TipoFeriado = tipoFeriado,
                            NivelFeriado = nivelFeriado
                        });
                    }
                    else
                    {
                        return Json(new { EhFeriado = false });
                    }
                }
                else
                {
                    // Se a chamada não foi bem-sucedida, tratar o erro conforme necessário
                    return Json(new { Erro = "Erro na chamada da API de feriados" });
                }
            }
        }

    }

}

public class Feriado
{
    public string Date { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Level { get; set; }
    // Adicione outras propriedades conforme necessário
}