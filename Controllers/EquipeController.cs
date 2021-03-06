using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EPlayers.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace EPlayers.Controllers
{
    public class EquipeController : Controller
    {
       
       Equipe equipeModel = new Equipe();

       /// <summary>
       /// Aponta para a Index da minha View
       /// </summary>
       /// <returns>a própria View da Index</returns>
       public IActionResult Index()
        {
            ViewBag.Equipes = equipeModel.ReadAll();
            return View();
        }

        /// <summary>
        ///  Cadastra uma nova equipe
        /// </summary>
        /// <param name="form">Dados do formulario</param>
        /// <returns>Redireciona para a mesma página </returns>
        public IActionResult Cadastrar(IFormCollection form)
        {
            Equipe equipe = new Equipe();
            equipe.IdEquipe = Int32.Parse( form["IdEquipe"]);
            equipe.Nome = form["Nome"];

            // Upload da imagem
             var file    = form.Files[0];
             var folder  = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

            //pastaA , pastaB , pastaC , arquivo.pdf

            if(file != null)
            {
                if(!Directory.Exists(folder)){
                    Directory.CreateDirectory(folder);
                }

                //wwwroot/img/Equipe/arquivo.pdf
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))  
                {  
                    file.CopyTo(stream);  
                }
                equipe.Imagem   = file.FileName;
            }
            else
            {
                equipe.Imagem   = "padrao.png";
            }
            // Fim - Upload Imagem

            equipeModel.Create(equipe);

            return LocalRedirect("~/Equipe");
        }
         
        [Route("[controller]/{id}")]
        public IActionResult Excluir(int id)
        {
         equipeModel.Delete(id);
         return LocalRedirect("~/Equipe");
        }


    }
}
