using ContasPagar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContasPagar.ViewModels
{
    public class RegraPost
    {
        #region Construtores
        public RegraPost(int diasAtraso, decimal multaPercentual, decimal jurosPercentual)
        {
            this.DiasMinimoAtraso = diasAtraso;
            this.MultaPercentual = multaPercentual;
            this.JurosDiarioPercentual = jurosPercentual;
        }

        public RegraPost()
        {
        }
        #endregion

        #region Atributos
        public int DiasMinimoAtraso { get; set; }

        public decimal MultaPercentual { get; set; }

        public decimal JurosDiarioPercentual { get; set; } 
        #endregion

        public Regra ToModel()
        {
            return new Regra
            {
                Id = Guid.NewGuid(),
                DiasMinimoAtraso = this.DiasMinimoAtraso,
                MultaPercentual = this.MultaPercentual,
                JurosDiarioPercentual = this.JurosDiarioPercentual
            };
        }
    }
}