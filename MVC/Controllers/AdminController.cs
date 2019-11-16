using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.DTO;
using BLL.Infrastructure;
using MVC.Models;
using AutoMapper;

namespace MVC.Controllers
{
    public class AdminController : Controller
    {
        IRouteService RouteService;

        public AdminController(IRouteService service)
        {
            RouteService = service;
        }
        // GET: Admin
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            ViewBag.DaysOfWeek = new DaysSelector();
            return View();

        }
        public ActionResult AddRoute(string NameStation)
        {

            return View("Index");
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddRoute(string routeName, DaysSelector daysOfWeek, string trainName, string trainNumber, string stations, string stationsTime, string carriages)
        {
            string[] CarriagesTypes = carriages.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);




            RouteDTO NewRoute = new RouteDTO()
            {
                Name = routeName,
                TrainName = trainName,
                TrainNumber = trainNumber,
                StationsNames = stations.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries),
                TimeOfStopPoints = stationsTime.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries),
                Days = daysOfWeek.SelectedDays()                
            };

            var a = daysOfWeek.SelectedDays();
            try
            {
                RouteService.AddRoute(NewRoute, CarriagesTypes);
            }
            catch(ValidationException e)
            {
                ViewBag.Message = e.Message;
            }
            
             
            return View("Index");
        }

        [Authorize(Roles ="admin")]
        public ActionResult GetTickets()
        {
            List<TicketViewModel> model = new List<TicketViewModel>();

            List<TicketDTO> ticketDTOs = RouteService.GetTickets();

            var TicketMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TicketDTO, TicketViewModel>();
                cfg.CreateMap<RouteDTO, RouteViewModel>();
                cfg.CreateMap<CarriageDTO, CarriageViewModel>();
            }).CreateMapper();

            model = TicketMapper.Map<List<TicketDTO>, List<TicketViewModel>>(ticketDTOs);

            return View(model);
        }
    }
}