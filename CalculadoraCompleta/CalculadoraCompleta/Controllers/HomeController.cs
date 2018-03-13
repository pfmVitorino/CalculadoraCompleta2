using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalculadoraCompleta.Controllers
{
    public class HomeController : Controller
    {

  
        // GET: Home
        public ActionResult Index()
        {
            // preparar os primeiros valores da calculadora

            ViewBag.Visor = "0";
            Session["operador"] = "";
            Session["limpaVisor"] = true;



            return View();
        }

        // POST : Home
        [HttpPost]

        public ActionResult Index(string bt, string visor)
        {
            //determinar a acçao a executar
            switch (bt)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "0":
                    // recupera o resultado da decisão sobre a limpeza do ecrã
                    bool limpaEcra = (bool)Session["limpaVisor"];
                    // processa a escrita do visor 
                    if (limpaEcra || visor.Equals("0")) visor = bt;
                    else visor += bt;
                    // marcar o visor para continuar a escrita do operando

                    Session["limpaVisor"] = false;
                    break;


                case "+/-":

                    visor = Convert.ToDouble(visor) * -1 + "";
                    break;


                case ",":

                    if (!visor.Contains(",")) visor += ",";

                    break;

                case "C":

                    visor = "0";
                    Session["operador"] = "";
                    Session["limpaVisor"] = true;
                    break;


                case "+":
                case "-":
                case "X":
                case ":":
                case "=":

                    // se não é a primera vez que pressiono um operardor
                    if (!((string)Session["operador"]).Equals(""))
                    {

                        
                    
                        // agora é que se vai fazer a conta
                        //obter os operandos
                        double primeiroOperando = Convert.ToDouble((string)Session["primeiroOperando"]);
                        double segundoOperando = Convert.ToDouble(visor);
                        // escolher a operação a fazer com o operador anterior
                        switch ((string)Session["operador"])
                        {
                            case "+":
                                visor = primeiroOperando + segundoOperando + "";
                                break;
                            case "-":
                                visor = primeiroOperando - segundoOperando + "";
                                break;
                            case "X":
                                visor = primeiroOperando * segundoOperando + "";
                                break;
                            case ":":
                                visor = primeiroOperando / segundoOperando + "";
                                break;
                        } // switch fechado
                    }
                    // preservar os valores fornecidos para operações futuras
                    if (bt.Equals("=")) Session["operador"] = "";
                    else Session["operador"] = bt;


                    Session["primeiroOperando"] = visor;

                        // marcar o visor para 'limpeza'
                        Session["limpaVisor"] = true;



                    
                    break;

                

            }

            // enviar o resultado para a view

            ViewBag.Visor = visor;


            return View();
        }
      
         





          
    }
}