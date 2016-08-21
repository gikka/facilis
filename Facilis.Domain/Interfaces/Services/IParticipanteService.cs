﻿using Facilis.Domain.Entities;
using System.Collections.Generic;

namespace Facilis.Domain.Interfaces.Services
{
    public interface IParticipanteService : IServiceBase<Participante>
    {
        IEnumerable<Participante> ListarPorUsuario(string usuarioId);
        IEnumerable<Participante> ListarInscritosPorEvento(int eventoId);
        void MarcarPresenca(int id);
        IEnumerable<Participante> ContarPorRegiao(int eventoId);
        IEnumerable<Participante> ContarPorSexo(int eventoId);

    }
}
