Apra efetuar os testes será necessário alterar a string de conexão no appsettings.json e rodar a  migration.

INSERT INTO [dbo].[Authors] ([Id], [Name])
VALUES
    (1, 'Autor 1'),
    (2, 'Autor 2'),
    (3, 'Autor 3'),
    (4, 'Autor 4');

    INSERT INTO [dbo].[Genres] ([Id], [Name])
VALUES
    (1, 'Fiction'),
    (2, 'Non-Fiction'),
    (3, 'Science Fiction'),
    (4, 'Fantasy');

INSERT INTO [dbo].[Books] ([Id], [Title], [GenreId], [AuthorId])
VALUES
    (1, 'Livro A', 1, 1),
    (2, 'Livro B', 2, 2),
    (3, 'Livro C', 3, 3),
    (4, 'Livro D', 4, 4);

