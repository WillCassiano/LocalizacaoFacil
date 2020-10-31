using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace LocalizacaoFacil.Models
{
    public class Produto
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string Referencia { get; set; }
        public string Endereco { get; set; }


        public static List<Produto> GetProdutos(BancoFirebird db, string marca, int fabricante)
        {
            if (String.IsNullOrWhiteSpace(marca))
                marca = null;

            List<Produto> produtos = new List<Produto>();
            db.Open();

            db.command = new FbCommand();
            db.command.Connection = db.connection;

            db.command.CommandText = @"select pdg.codigo,
                                              pdg.descricao,
                                              pdg.referencia,
                                              estq.endereco,
                                              pdg.marca,
                                              pdg.fabricante
                                         from testprodutogeral pdg
                                         join testestoque estq on estq.produto = pdg.codigo
                                        where estq.empresa = '01'
                                          and pdg.marca      = coalesce(@marca , pdg.marca)
                                          and pdg.fabricante = iif(@fabricante = 0, pdg.fabricante, @fabricante)";

            db.command.Parameters.Clear();
            db.command.Parameters.AddWithValue("@marca", marca);
            db.command.Parameters.AddWithValue("@fabricante", fabricante);

            var reader = db.command.ExecuteReader();

            while (reader.Read())
            {
                produtos.Add(new Produto()
                {
                    Codigo = reader.GetString(0),
                    Descricao = reader.GetString(1),
                    Referencia = reader.GetString(2),
                    Endereco = reader.GetString(3)
                });
            }

            return produtos;
        }
    }
}
