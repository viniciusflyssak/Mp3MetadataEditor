using System;
using TagLib;

namespace Mp3MetadataEditor
{
    public class MetadataEditor
    {
        public bool AlterarMetadados(string caminhoArquivo, string artista, string banda, string compositor, string album, string caminhoFoto)
        {
            try
            {
                // Carregar o arquivo MP3
                var arquivo = TagLib.File.Create(caminhoArquivo);

                // Alterar os campos de metadados
                arquivo.Tag.Performers = new[] { artista };
                arquivo.Tag.AlbumArtists = new[] { banda };
                arquivo.Tag.Composers = new[] { compositor };
                arquivo.Tag.Album = album;

                // Carregar a imagem e definir como capa
                if (!string.IsNullOrEmpty(caminhoFoto))
                {
                    var fotoBytes = System.IO.File.ReadAllBytes(caminhoFoto);
                    var picture = new TagLib.Picture(new TagLib.ByteVector(fotoBytes));
                    picture.Type = PictureType.FrontCover;
                    picture.Description = "";
                    picture.MimeType = System.Net.Mime.MediaTypeNames.Image.Jpeg;

                    arquivo.Tag.Pictures = new[] { picture };
                }

                // Salvar as alterações
                arquivo.Save();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return false;
            }
        }
    }
}
