﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LanguageDetection._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    </asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
	<div id="inputContainer">
        <div id="hiddenArea">
            <span class="hiddenWord"></span>
        </div>
        <asp:TextBox ID="TextBox1" CssClass ="LanguageDetectorArea" MaxLength="94" runat="server" EnableTheming="True" Wrap="False"></asp:TextBox>
	</div>
    <table id="toolTip" class="simple-little-table">
        <thead>
            <tr>
                <td>Language</td>
                <td>Probability percentage</td>
            </tr>
        </thead>
        <tbody>
		    <tr>
			    <td>Text</td>
			    <td>Language</td>
			    <td>Percent</td>
		    </tr>
        </tbody>
	</table>
	
	<link rel="stylesheet" href="/Content/LanguageDetecor.CSS/Default.css" type="text/css"/>
	<script src="/Scripts/LanguageDetector.Scripts/LanguageDetector.js" type="text/javascript"></script>
    </asp:Content>
