
(function ($) {
	'use strict';

	// Preloader
	$(window).on('load', function(){
		$("#preloader").removeClass("loader_show");
		$("#preloader").addClass("hide");
		$(".loader").addClass("fadeout");
		$("body").addClass("enable_page");
	})

	// Custom mouse cursor
	document.getElementsByTagName("body")[0].addEventListener("mousemove", function(n) {
		e.style.left = n.clientX + "px",
		e.style.top = n.clientY + "px"
	});
	var	e = document.getElementById("bnz-pointer");

	$(document).mousemove(function(e) {

		$(".slick-next, .slick-prev, input.button, a")
		.on("mouseenter", function() {
			$('.bnz-pointer').addClass("bnz-large")
		})
		.on("mouseleave", function() {
			$('.bnz-pointer').removeClass("bnz-large")
		})

		$(".slick-dots li button, .filters-button-group button, .form-control")
		.on("mouseenter", function() {
			$('.bnz-pointer').addClass("bnz-small")
		})
		.on("mouseleave", function() {
			$('.bnz-pointer').removeClass("bnz-small")
		})

		$(".slick-slide")
		.on("mouseenter", function() {
			$('.bnz-pointer').addClass("bnz-drag")
		})
		.on("mouseleave", function() {
			$('.bnz-pointer').removeClass("bnz-drag")
		})

		$(".bnz-pointer-none")
		.on("mouseenter", function() {
			$('.bnz-pointer').addClass("bnz-none")
		})
		.on("mouseleave", function() {
			$('.bnz-pointer').removeClass("bnz-none")
		})

	});

	// Popup Reserve Table
	$('.reserve_button_group a.button').on('click', function (e) {
		e.preventDefault();
		$('.reserve_table_popup').toggleClass('active');
	});
	$('.popup_close').on('click', function (e) {
		e.preventDefault();
		$('.reserve_table_popup').removeClass('active');
	});

	// Side Menu
	$(document).ready(function () {
		ma5menu({
			menu: '.main_menu',
			activeClass: 'current',
			footer: '#ma5menu-tools',
			position: 'right',
			closeOnBodyClick: true
		});
	});

	// Slider Slick
	$('.slider_slick').slick({
        infinite: false,
        autoplay: true,
        speed: 1000,
        cssEase: 'linear',
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: true,
        dots: true,
        vertical: false,
        verticalSwiping: false,
	});


	// Testimonial Slick
	$('.testimonial_slick').slick({
		infinite: false,
		autoplay: false,
		speed: 600,
		slidesToShow: 1,
		slidesToScroll: 1,
		centerPadding: 0,
		arrows: true,
		dots: false,
		responsive: [
			{
				breakpoint: 1024,
				settings: {
					slidesToShow: 1,
					slidesToScroll: 1,
					arrows: false,
					dots: true
				}
			},
			{
				breakpoint: 767,
				settings: {
					slidesToShow: 1,
					slidesToScroll: 1,
					arrows: false,
					dots: true
				}
			},
			{
				breakpoint: 600,
				settings: {
					slidesToShow: 1,
					slidesToScroll: 1,
					arrows: false,
					dots: true
				}
			}
		]
	});


	// Product Slick
	$('.product_slick').slick({
		infinite: false,
		autoplay: false,
		speed: 600,
		slidesToShow: 4,
		slidesToScroll: 1,
		arrows: true,
		dots: false,
		responsive: [
			{
				breakpoint: 1024,
				settings: {
					slidesToShow: 3,
					slidesToScroll: 1,
					arrows: false,
					dots: true
				}
			},
			{
				breakpoint: 767,
				settings: {
					slidesToShow: 2,
					slidesToScroll: 1,
					arrows: false,
					dots: true
				}
			},
			{
				breakpoint: 600,
				settings: {
					slidesToShow: 1,
					slidesToScroll: 1,
					arrows: false,
					dots: true
				}
			}
		]
	});



	// Youtube
	var $ytvideoTrigger = $(".ytplay_btn");
	$ytvideoTrigger.on("click", function(evt) {  
		$(".ytube_video").addClass("play");
		$("#ytvideo")[0].src += "?autoplay=1";
	});

	
	// Local
	var $videoTrigger = $(".play_btn");
	var $videoContainer = $(".local_video");
	$videoTrigger.on("click", function(evt) {  
		if ($videoContainer.hasClass("play")) {
			$videoContainer
			.removeClass("play")
			.find("video").get(0).pause();
		}
		else {
			$videoContainer
			.addClass("play")
			.find("video").get(0).play();
		}
	});


	// Isotope Portfolio
	var $grid = $('.grid-masonary').isotope({
		itemSelector: '.grid-item', 
		percentPosition: true,
		layoutMode: 'packery',
		transformsEnabled: true,
		transitionDuration: "700ms",
		fitWidth: true,
		columnWidth: '.grid-sizer',
	});

	$grid.imagesLoaded().progress( function() {
		$grid.isotope('layout');
	});

	var iso = $grid.data('isotope');
	// bind filter button click
	$('.filters-button-group .button').on( 'click', function() {
		var filterValue = $( this ).attr('data-filter');
		// use filterFn if matches value
		$grid.isotope({ filter: filterValue });
	});

	// change is-checked class on buttons
	$('.button-group').each( function( i, buttonGroup ) {
		var $buttonGroup = $( buttonGroup );
		$buttonGroup.on( 'click', 'button', function() {
			$buttonGroup.find('.is-checked').removeClass('is-checked');
			$( this ).addClass('is-checked');
		});
	});


	// Video Popup
	$('[data-fancybox="video"]').fancybox({
		arrows: true,
		animationEffect: [
		  //"false",            - disable
		  //"fade",
		  //"slide",
		  //"circular",
		  //"tube",
		  //"zoom-in-out",
		  "rotate"
		],
		transitionEffect: [
		  //"false",            - disable
		  //"fade",
		  //"slide",
		  "circular",
		  //"tube",
		  //"zoom-in-out",
		  //"rotate"
		],
		buttons: [
		  "zoom",
		  //"share",
		  //"slideShow",
		  "fullScreen",
		  //"download",
		  //"thumbs",
		  "close"
		],
		infobar: false,
	});
	
	// Image Popup
	$('[data-fancybox="images"]').fancybox({
		arrows: true,
		animationEffect: [
		  //"false",            - disable
		  //"fade",
		  //"slide",
		  "circular",
		  //"tube",
		  //"zoom-in-out",
		  //"rotate"
		],
		transitionEffect: [
		  //"false",            - disable
		  //"fade",
		  //"slide",
		  //"circular",
		  //"tube",
		  //"zoom-in-out",
		  //"rotate"
		],
		buttons: [
		  "zoom",
		  //"share",
		  //"slideShow",
		  "fullScreen",
		  //"download",
		  //"thumbs",
		  "close"
		],
		infobar: true,
	});

	  

	// Tab
	$('.foodmenu_tab_button_group > li > a').eq(0).addClass( "selected" );
	$('.foodmenu_tab_container > .foodmenu_tab_info').eq(0).css({'display':'block'});
	$('.foodmenu_tab_button_group').on("click",function(e){
		if($(e.target).is("a")){
	  
			/*Handle Tab Nav*/
			$('.foodmenu_tab_button_group > li > a').removeClass( "selected");
			$(e.target).addClass( "selected");
			
			/*Handles Tab Content*/
			var clicked_index = $("a",this).index(e.target);
			$('.foodmenu_tab_container > .foodmenu_tab_info').css({'display':'none'});
			$('.foodmenu_tab_container > .foodmenu_tab_info').eq(clicked_index).fadeIn();
		
		}
		$(this).blur();
		return false;
	});

	// Product Single Tab
	$('.single_tab_button_group > li > a').eq(0).addClass( "selected" );
	$('.single_tab_container > .single_tab_info').eq(0).css({'display':'block'});
	$('.single_tab_button_group').on("click",function(e){
		if($(e.target).is("a")){
	
			/*Handle Tab Nav*/
			$('.single_tab_button_group > li > a').removeClass( "selected");
			$(e.target).addClass( "selected");
			
			/*Handles Tab Content*/
			var clicked_index = $("a",this).index(e.target);
			$('.single_tab_container > .single_tab_info').css({'display':'none'});
			$('.single_tab_container > .single_tab_info').eq(clicked_index).fadeIn();
		
		}
		$(this).blur();
		return false;
	});

	// Product Zoom
	$('.product_zoom_button_group > li > a').eq(0).addClass( "selected" );
	$('.product_zoom_container > .product_zoom_info').eq(0).css({'display':'block'});
	$('.product_zoom_button_group').on("click",function(e){
		if($(e.target).is("a")){

			/*Handle Tab Nav*/
			$('.product_zoom_button_group > li > a').removeClass( "selected");
			$(e.target).addClass( "selected");
			
			/*Handles Tab Content*/
			var clicked_index = $("a",this).index(e.target);
			$('.product_zoom_container > .product_zoom_info').css({'display':'none'});
			$('.product_zoom_container > .product_zoom_info').eq(clicked_index).fadeIn();
		}
		$(this).blur();
		return false;
	});

	//Single Product Price Calculate
	function CalcPrice (qty){
		var price = $(".product_per_price").attr("data-content");
		var total = parseFloat((price * qty)).toFixed(2);
		$(".product_total_price").text(total);
	}
	$(document).on("click", ".product_quantity_subtract", function(e){
		var value = $("#product_quantity_input").val();
		var newValue = parseInt(value) - 1;
		if(newValue < 0) newValue=0;
		$("#product_quantity_input").val(newValue);
		CalcPrice(newValue);
	});
	$(document).on("click", ".product_quantity_add", function(e){
		var value = $("#product_quantity_input").val();
		var newValue = parseInt(value) + 1;
		$("#product_quantity_input").val(newValue);
		CalcPrice(newValue);
	});
	$(document).on("blur", "#product_quantity_input", function(e){
		var value = $("#product_quantity_input").val();
		CalcPrice(value);
	});

	// Cart open
	$('a.cart_icon').on('click', function (e) {
		e.preventDefault();
		$('.cart_box').toggleClass('active');
	});

	// Select2 JS
	$(".select_dropdown_value").select2();

	// Coupon open
	$('.opencoupon a.button').on('click', function (e) {
		e.preventDefault();
		$('.couponform').toggleClass('active');
	});

	// Totop Button
	$('.totop a').on('click', function(e) {
		e.preventDefault();
		$('html, body').animate({scrollTop: 0}, '500');
	});


	// AOS Initialize
	AOS.init({
		once: true,
	});

	// Parallax
	$('.jarallax').jarallax({
		speed: 0.2,
		keepImg: true,
	});

	// Header Sticky
	var header = $(".header");
    $(window).scroll(function() {
        var scroll = $(window).scrollTop();

        if (scroll >= 120) {
            header.addClass("sticky");
        } else {
            header.removeClass("sticky");
        }
    });
	
	
})(jQuery);

// Hide header on scroll down
var didScroll;
var lastScrollTop = 0;
var navbarHeight = $('header').outerHeight();

$(window).scroll(function(event){
	didScroll = true;
});

setInterval(function() {
	if (didScroll) {
		hasScrolled();
		didScroll = false;
	}
}, 50);

function hasScrolled() {
	var st = $(this).scrollTop();
	

	if (st > lastScrollTop && st > navbarHeight){
		// Scroll Down
		$('header .top_bar').addClass('top-up');
	} else {
		// Scroll Up
		$('header .top_bar').removeClass('top-up');
	}
	
	lastScrollTop = st;
}
// End Sticky Header
