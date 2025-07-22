using System.Text.Json;
using HackatonFiapNETT6.AgendaMedica.Messaging.Usuarios;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Entities;
using HackatonFiapNETT6.AgendaMedica.Workers.Cadastro.Workers.Infrastructure;
using Microsoft.AspNetCore.Identity.Data;

namespace HackatonFiapNETT6.AgendaMedica.Workers.Cadastro.Workers;

public class UsuarioCreatedEventConsumer
{
    private readonly IUsuarioWriteRepository _repository;

    public UsuarioCreatedEventConsumer(IUsuarioWriteRepository repository) => _repository = repository;

    public async Task ProcessarAsync(string mensagem)
    {
        var evento = JsonSerializer.Deserialize<CriarUsuarioMessage>(mensagem);
        if (evento != null)
            await _repository.InserirAsync(new Usuario(evento.UsuarioId,
                    evento.LoginCriptografado,
                    evento.SenhaHash,
                    evento.Salt,
                    evento.Tipo)
                , CancellationToken.None);
    }
}