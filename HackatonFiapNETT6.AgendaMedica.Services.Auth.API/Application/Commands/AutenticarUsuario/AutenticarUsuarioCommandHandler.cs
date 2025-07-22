using HackatonFiapNETT6.AgendaMedica.Services.Auth.Application.Responses;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Entities;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Events;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Exceptions;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Domain.Interfaces;
using HackatonFiapNETT6.AgendaMedica.Services.Auth.Infrastructure.Security;
using MediatR;

namespace HackatonFiapNETT6.AgendaMedica.Services.Auth.Application.Commands.AutenticarUsuario;

public class AutenticarUsuarioCommandHandler : IRequestHandler<AutenticarUsuarioCommand, TokenResponse>
{
    private readonly IUsuarioReadOnlyRepository _usuarioReadOnlyRepository;
    private readonly IMediator _mediator;
    private readonly JwtTokenGenerator _jwtTokenGenerator;
    
    
    public AutenticarUsuarioCommandHandler(IUsuarioReadOnlyRepository usuarioReadOnlyRepository, IMediator mediator, JwtTokenGenerator jwtTokenGenerator)
    {
        _usuarioReadOnlyRepository = usuarioReadOnlyRepository;
        _mediator = mediator;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<TokenResponse> Handle(AutenticarUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioReadOnlyRepository.ObterPorLoginAsync(request.Login, request.Tipo);
        if (usuario == null || !usuario.ValidarSenha(request.Senha))
            throw new CredenciaisInvalidasException();

        var token = GerarJwt(usuario);

        await _mediator.Publish(new UsuarioAutenticadoEvent(usuario.Id, usuario.Tipo));

        return new TokenResponse(token, DateTime.UtcNow.AddHours(1));
    }

    private string GerarJwt(Usuario usuario)
    {
        var token = _jwtTokenGenerator.GerarToken(usuario);
        return token;
    }
}