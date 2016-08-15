﻿using AutoMapper;
using Facilis.Application.Interface;
using Facilis.MVC.ViewModels;
using Facilis.Domain.Entities;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Facilis.MVC.Controllers
{
    [Authorize]
    public class EventosController : Controller
    {
        // GET: Evento
        private readonly IEventoAppService _eventoApp;
        private readonly IArquivoAppService _arquivoApp;
        private readonly IVideoAppService _videoApp;

        public EventosController(IEventoAppService eventoApp, IArquivoAppService arquivoApp, IVideoAppService videoApp)
        {
            _eventoApp = eventoApp;
            _arquivoApp = arquivoApp;
            _videoApp = videoApp;
        }
        

        public ActionResult Index()
        {
            var listaEventos = _eventoApp.ListarPorUsuario(User.Identity.GetUserId()); 
            var eventoViewModel = Mapper.Map<IEnumerable<Evento>, IEnumerable<EventoViewModel>>(listaEventos);

            return View(eventoViewModel); 
        }

        // GET: Evento/Details/5
        public ActionResult Details(int id)
        {
            var evento = _eventoApp.GetById(id);
            var eventoViewModel = Mapper.Map<Evento, EventoViewModel>(evento);

            return View(eventoViewModel);
        }

        // GET: Evento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Evento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventoViewModel evento)
        {
            if (ModelState.IsValid)
            {
                var eventoDomain = Mapper.Map<EventoViewModel, Evento>(evento);
                eventoDomain.UsuarioId = User.Identity.GetUserId();
                _eventoApp.Add(eventoDomain);

                return RedirectToAction("Index");
            }

            return View(evento);
        }

        // GET: Evento/Edit/5
        public ActionResult Edit(int id)
        {
            var evento = _eventoApp.GetById(id);
            evento.Arquivos = _arquivoApp.ListarPorEvento(id);
            evento.Videos = _videoApp.ListarPorEvento(id);
            var eventoViewModel = Mapper.Map<Evento, EventoViewModel>(evento);

            return View(eventoViewModel);
        }

        // POST: Evento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EventoViewModel evento)
        {
            if (ModelState.IsValid)
            {
                var eventoDomain = Mapper.Map<EventoViewModel, Evento>(evento);
                eventoDomain.UsuarioId = User.Identity.GetUserId();
                _eventoApp.Update(eventoDomain);

                return RedirectToAction("Index");
            }

            return View(evento);
        }

        // GET: Evento/Delete/5
        public ActionResult Delete(int id)
        {
            var evento = _eventoApp.GetById(id);
            var eventoViewModel = Mapper.Map<Evento, EventoViewModel>(evento);

            return View(eventoViewModel);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var evento = _eventoApp.GetById(id);
            _eventoApp.Remove(evento);

            return RedirectToAction("Index");
        }

        // GET: Evento/DetalhesInscricao
        [AllowAnonymous]
        public ActionResult DetalhesInscricao(int id)
        {
            var evento = _eventoApp.GetById(id);
            var eventoViewModel = Mapper.Map<Evento, EventoViewModel>(evento);

            return View(eventoViewModel);
        }

        private void CarregarDropDownTipoEvento()
        {
            var TipoEventoList = new List<dynamic>();
            TipoEventoList.Add(new { Id = "", Text = "" });
            TipoEventoList.Add(new { Id = "1", Text = "Gratuito" });
            TipoEventoList.Add(new { Id = "2", Text = "Pago" });
            ViewBag.SexoList = new SelectList(TipoEventoList, "Id", "Text");

        }

    }
}
