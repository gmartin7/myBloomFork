/// <reference path="../../typings/jquery/jquery.d.ts" />
import * as $ from 'jquery';
import {restoreToolboxSettings} from './toolbox';

//this is currently inserted by c#. TODO: get settings via ajax
declare function GetToolboxSettings():any;

$(document).ready(function() { 
    restoreToolboxSettings(GetToolboxSettings()); 
});