using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Infrastructure;
using AutoMapper;
using MVC.Models;
using System.Globalization;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        IRouteService RouteService;
        public HomeController(IRouteService service)
        {
            RouteService = service;
        }
        public ActionResult Index()
        {

            return View();
        }


        [HttpGet]
        public ActionResult Search(string FromStation, string ToStation, string date)
        {
            DateTime Date = DateTime.Parse(date);
            List<RouteDTO> RoutesDTO = new List<RouteDTO>();
            try
            {
                RoutesDTO = RouteService.SearchRoute(FromStation, ToStation, Date);
                
            }
            catch(ValidationException e)
            {
                ViewBag.ValidMessege = e.Message;
                return View("Index");
            }

            var RouteListMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RouteDTO, RouteViewModel>();
                cfg.CreateMap<CarriageDTO, CarriageViewModel>();
            }).CreateMapper();
            List<RouteViewModel> FoundRoutes = RouteListMapper.Map<List<RouteDTO>, List<RouteViewModel>>(RoutesDTO);

            foreach (var route in FoundRoutes)
            {
                var Groups = from c in route.Carriages group c by c.Type;
                route.CarriageGroups = Groups.ToList();
            }

            ResultOfSearchViewModel Result = new ResultOfSearchViewModel();
            Result.FoundRoutes = FoundRoutes;
            Result.From = FromStation;
            Result.To = ToStation;
            Result.Date = Date;


            return View(Result);
        }
        [HttpPost]
        public ActionResult SelectSeats(string RouteId, string CarriagesType)
        {
            RouteDTO route = RouteService.GetRoute(int.Parse(RouteId));
            var RouteListMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RouteDTO, RouteViewModel>();
                cfg.CreateMap<CarriageDTO, CarriageViewModel>();
            }).CreateMapper();
            var routeVM = RouteListMapper.Map<RouteDTO, RouteViewModel>(route);

            var Group = routeVM.Carriages.Where(x => x.Type == CarriagesType).ToList();

            return PartialView(Group);
        }
        [HttpPost]
        public ActionResult SelectWagons(string RouteId, string Number, string Wagons, string Seats,string operation)
        {
            SeatSelectionViewModel model = new SeatSelectionViewModel();

            RouteDTO route = RouteService.GetRoute(int.Parse(RouteId));
            var RouteListMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RouteDTO, RouteViewModel>();
                cfg.CreateMap<CarriageDTO, CarriageViewModel>();
            }).CreateMapper();

            model.Route = RouteListMapper.Map<RouteDTO, RouteViewModel>(route);            
            if (Wagons.Contains(","))
            {
                model.SelectedWagons = Wagons.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                model.SelectedSeats = Seats.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            else
            {
                model.SelectedWagons= new List<string>();
                model.SelectedWagons.Add(Wagons);
                model.SelectedSeats = new List<string>();
                model.SelectedSeats.Add(Seats);
            }
            ;
            model.SelectedSeats.Add(RouteService.GetSeat(int.Parse(RouteId), int.Parse(Number)).ToString());
            model.SelectedWagons.Add(Number);
            model.SelectedSeats.RemoveAll(x => x == "");
            model.SelectedWagons.RemoveAll(x => x == "");

            return PartialView("SelectWagons", model);
        }
        public ActionResult RemoveSelectedWagon(string RouteId, string index, string Wagons, string Seats)

        {
            SeatSelectionViewModel model = new SeatSelectionViewModel();

            RouteDTO route = RouteService.GetRoute(int.Parse(RouteId));
            var RouteListMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RouteDTO, RouteViewModel>();
                cfg.CreateMap<CarriageDTO, CarriageViewModel>();
            }).CreateMapper();

            model.Route = RouteListMapper.Map<RouteDTO, RouteViewModel>(route);
            if (Wagons.Contains(","))
            {
                model.SelectedWagons = Wagons.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                model.SelectedSeats = Seats.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                RouteService.ToFreeSeat(route.Id, int.Parse(model.SelectedWagons[int.Parse(index)]));
                model.SelectedSeats.RemoveAt(int.Parse(index));
                model.SelectedWagons.RemoveAt(int.Parse(index));
            }
            else
            {
                RouteService.ToFreeSeat(route.Id, int.Parse(Wagons));
                model.SelectedWagons = new List<string>();
                model.SelectedSeats = new List<string>();
            }
            
            return PartialView("SelectWagons", model);
        }
        public ActionResult BuyOrder(int RouteId, string Wagons, string Seats)
        {
            List<TicketDTO> Tickets = new List<TicketDTO>();
            if (Wagons.Contains(","))
            {
                string[] WagonsList = Wagons.Split(',');
                string[] SeatsList = Seats.Split(',');
                for(int i=0; i<WagonsList.Length; i++)
                {
                    Tickets.Add(RouteService.BuyTicket(RouteId, int.Parse(WagonsList[i]), int.Parse(SeatsList[i])));
                }
            }
            else
            {
                Tickets.Add(RouteService.BuyTicket(RouteId, int.Parse(Wagons), int.Parse(Seats)));
            }

            var TicketMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TicketDTO, TicketViewModel>();
                cfg.CreateMap<RouteDTO, RouteViewModel>();
                cfg.CreateMap<CarriageDTO, CarriageViewModel>();
            }).CreateMapper();
            var model = TicketMapper.Map<List<TicketDTO>, List<TicketViewModel>>(Tickets);
            return View(model);
        }



    }
}