namespace FabulousMultiNavPages

open System.Diagnostics
open Fabulous
open Fabulous.XamarinForms
open Fabulous.XamarinForms.LiveUpdate
open Xamarin.Forms

module AppPages =    
    type Name = Name of string
    let nameValue (Name str) = str
    type Names = {
        TemplatePage: Name
        StartPage: Name
        TrainingPage: Name    
        FirstPage: Name
        SecondPage: Name
    }
    let names: Names = {
        TemplatePage = Name "Template Page"
        StartPage = Name "Start Page"
        TrainingPage = Name "Training Page"
        FirstPage = Name "First Page"
        SecondPage = Name "Second Page"
    }

type GlobalModel = { 
    PageStash: List<AppPages.Name>
    }

module Helpers = 
    let rec reshuffle list: List<'a> =
        match list with
        | [] -> []
        | l -> l |> List.rev |> List.tail |> List.rev