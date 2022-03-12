namespace FabulousMultiNavPages

open System.Diagnostics
open Fabulous
open Fabulous.XamarinForms
open Fabulous.XamarinForms.LiveUpdate
open Xamarin.Forms

type GlobalModel = { 
    PageStash: List<string>
    }

module Helpers = 
    let rec reshuffle list: List<'a> =
        match list with
        | [] -> []
        | l -> l |> List.rev |> List.tail |> List.rev