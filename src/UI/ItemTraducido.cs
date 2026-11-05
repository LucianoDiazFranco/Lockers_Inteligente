using LockersInteligentes.ArqBase.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.UI
{
    public class ItemTraducido
    {
        public object Valor { get; set; }
        public string Texto { get; set; }

        public override string ToString()
        {
            return Texto;
        }

        /// <summary>Arma la lista traducida de todos los valores de un enum.</summary>
        public static List<ItemTraducido> DesdeEnum(Type tipoEnum, string prefijo)
        {
            List<ItemTraducido> items = new List<ItemTraducido>();

            foreach (object valor in Enum.GetValues(tipoEnum))
            {
                items.Add(new ItemTraducido
                {
                    Valor = valor,
                    Texto = GestorIdioma.Instancia.TraducirEnum(prefijo, valor)
                });
            }

            return items;
        }
    }
}
