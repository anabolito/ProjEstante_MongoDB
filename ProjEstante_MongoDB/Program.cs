using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ProjEstante_MongoDB;
using System.Net.Http.Headers;

internal class Program
{
    private static void Main(string[] args)
    {
        MongoClient mongo = new MongoClient("mongodb://localhost:27017");

        var basededados = mongo.GetDatabase("estante");
        var collection = basededados.GetCollection<BsonDocument>("livros");
        //var collectionLend = basededados.GetCollection<BsonDocument>("emprestado");
        //var collectionreading = basededados.GetCollection<BsonDocument>("lendo");
        //var collectionReaded = basededados.GetCollection<BsonDocument>("lidos");

        var books = collection.Find(new BsonDocument()).ToList();
        int opc;

        do
        {
            opc = Menu();

            switch (opc)
            {
                case 1:  // cadastrar livro
                    Console.Clear();
                    collection.InsertOne(InsertBook());
                    Console.WriteLine("Livro adicionado à estante com sucesso S2");
                    break;

                case 2:  // editar livro
                    Console.Clear();
                    EditBook();

                    break;

                case 3: // remover livro 
                    Console.Clear();
                    Console.WriteLine("Digite o título do livro a ser removido:");
                    string titleToBeDeleted = Console.ReadLine();
                    var toBeDelete = Builders<BsonDocument>.Filter.Regex("titulo", titleToBeDeleted);
                    break;

                case 4: // mostrar estante
                    Console.Clear();
                    foreach (var book in books)
                    {
                        var bookFinded = BsonSerializer.Deserialize<Book>(book);
                        Console.WriteLine($"{book.ToString()}");
                    }
                    break;

                case 5://Emprestar Livro
                    Console.Clear();
                    break;

                case 6: //Adicionar livro ao status de sendo lido
                    Console.Clear();
                    break;

                case 7: // sair do programa
                    Console.Clear();
                    Console.WriteLine("Até mais :)");
                    Thread.Sleep(1000);
                    break;
            }

        } while (opc != 5);



        //foreach (var book in books)
        //{
        //    var Book = BsonSerializer.Deserialize<BsonDocument>(book);
        //    Console.WriteLine($"{book}");
        //}

        BsonDocument InsertBook()
        {
            Console.Write("Título: "); string t = Console.ReadLine();

            Console.Write("Editora: "); string p = Console.ReadLine();

            Console.Write("Ano de publicação: "); int y = int.Parse(Console.ReadLine());

            Console.Write("Quantos autores? "); int qtdAuthors = int.Parse(Console.ReadLine());

            string[] authors;
            authors = new string[qtdAuthors];

            for (int i = 0; i < qtdAuthors; i++)
            {
                Console.Write($"Autor{i + 1}: ");
                authors[i] = Console.ReadLine();
            }

            Book book = new(t, p, y, authors);

            Console.WriteLine(book.ToString());

            var dr = new BsonDocument
            {
            {"titulo", book.Title },
            {"editora", book.Publisher},
            {"anoPublicacao", book.YearOfPublication },
            {"autores", book.Authors[qtdAuthors -1]},
            };

            Console.WriteLine(dr);

            return dr;
        }

        void EditBook()
        {
            Console.WriteLine("Título do livro a ser editado:");
            string titleToBeEdit = Console.ReadLine();

            var toBeEditMongo = Builders<BsonDocument>.Filter.Regex("titulo", titleToBeEdit);

            foreach (var book in books)
            {
                var bookFindedDB = BsonSerializer.Deserialize<Book>(book);
                Console.WriteLine(book.ToString());
            }

           
            Console.WriteLine("(1)-Título\n(2)-Editora\n(3)-Ano de Publicação\n(4)-Autores\n\nQual campo deseja alterar?");
            int x = int.Parse(Console.ReadLine());
            if (x == 1)
            {
                Console.Write("Novo Título: ");
                string title = Console.ReadLine();

                var newInfo = Builders<BsonDocument>.Update.Set("titulo", title);
                collection.UpdateOne(toBeEditMongo, newInfo);

            }
            if (x == 2)
            {
                Console.Write("Nova Editora: ");
                string publisher = Console.ReadLine();

                var newInfo = Builders<BsonDocument>.Update.Set("editora", publisher);
                collection.UpdateOne(toBeEditMongo, newInfo);
            }
            if (x == 3)
            {

                Console.Write("Novo Ano de Publicação: ");
                int year = int.Parse(Console.ReadLine());

                var newInfo = Builders<BsonDocument>.Update.Set("anoPublicacao", year);
                collection.UpdateOne(toBeEditMongo, newInfo);
            }
            if (x == 4)
            {

                Console.Write("Quantos autores? "); int qtdAuthors = int.Parse(Console.ReadLine());
                
                Console.Write("Novos autores: ");

                string[] authors;
                authors = new string[qtdAuthors];

                for (int i = 0; i < qtdAuthors; i++)
                {
                    Console.Write($"Autor{i + 1}: ");
                    authors[i] = Console.ReadLine();
                }

                var newInfo = Builders<BsonDocument>.Update.Set("autores", authors[qtdAuthors]);
                collection.UpdateOne(toBeEditMongo, newInfo);

            }
        }

    }
    static public int Menu()
    {
        int i = 0;
        Console.Write("\n\nMENU DE OÇÕES:" +
                                "\n1 - Inserir Livro" +
                                "\n2 - Editar Livro" +
                                "\n3 - Remover Livro" +
                                "\n4 - Mostrar Estante" +
                                "\n5 - Emprestar Livro" +
                                "\n6 - Adicionar livro ao status de sendo lido" +
                                "\n7 - Sair " +
                                "\n\nEscolha uma opção:");
        i = int.Parse(Console.ReadLine());
        return i;
    }
}

