(function ($) {
    "use strict";
    //new Darkmode().showWidget();
    /*-------------------------------------
        Contact Form initiating
    -------------------------------------*/
    //var contactForm = $('#contact-form');
    //if (contactForm.length) {
    //    contactForm.validator().on('submit', function (e) {
    //        var $this = $(this),
    //            $target = contactForm.find('.form-response');
    //        if (e.isDefaultPrevented()) {
    //            $target.html("<div class='alert alert-danger'><p>Please select all required field.</p></div>");
    //        } else {
    //            $.ajax({
    //                url: "vendor/php/form-process.php",
    //                type: "POST",
    //                data: contactForm.serialize(),
    //                beforeSend: function () {
    //                    $target.html("<div class='alert alert-info'><p>Loading ...</p></div>");
    //                },
    //                success: function (response) {
    //                    var res = JSON.parse(response);
    //                    console.log(res);
    //                    if (res.success) {
    //                        $this[0].reset();
    //                        $target.html("<div class='alert alert-success'><p>Message has been sent successfully.</p></div>");
    //                    } else {
    //                        if (res.message.length) {
    //                            var messages = null;
    //                            res.message.forEach(function (message) {
    //                                messages += "<p>" + message + "</p>";
    //                            });
    //                            $target.html("<div class='alert alert-success'><p>" + messages + "</p></div>");
    //                        }
    //                    }
    //                },
    //                error: function () {
    //                    $target.html("<div class='alert alert-success'><p>Error !!!</p></div>");
    //                }
    //            });
    //            return false;
    //        }
    //    });
    //}

    /*-------------------------------------
    Section background image
    -------------------------------------*/
    $("[data-bg-image]").each(function () {
        var img = $(this).data("bg-image");
        $(this).css({
            backgroundImage: "url(" + img + ")"
        });
    });

    /*-------------------------------------
    Page Preloader
    -------------------------------------*/
    $("#preloader").fadeOut("slow", function () {
        $(this).remove();
    });

    /*-------------------------------------
    Popup
    -------------------------------------*/
    var yPopup = $(".popup-youtube");
    if (yPopup.length) {
        yPopup.magnificPopup({
            disableOn: 700,
            type: 'iframe',
            mainClass: 'mfp-fade',
            removalDelay: 160,
            preloader: false,
            fixedContentPos: false
        });
    }

    /*-------------------------------------
    Counter
    -------------------------------------*/
    var counterContainer = $('.counter');
    if (counterContainer.length) {
        counterContainer.counterUp({
            delay: 50,
            time: 2000
        });
    }

    /*-------------------------------------
    Circle Bars - Knob
    -------------------------------------*/
    if (typeof ($.fn.knob) !== undefined) {
        $('.knob.knob-percent.dial').each(function () {
            var $this = $(this),
                knobVal = $this.attr('data-rel');
            $this.knob({
                'draw': function () { },
                'format': function (value) {
                    return value + '%';
                }
            });
            $this.appear(function () {
                $({
                    value: 0
                }).animate({
                    value: knobVal
                }, {
                    duration: 2000,
                    easing: 'swing',
                    step: function () {
                        $this.val(Math.ceil(this.value)).trigger('change');
                    }
                });
            }, {
                accX: 0,
                accY: -150
            });
        });
    }

    //scrol
    //$("body").click(function () {
    //     if ($(".meanmenu-reveal").hasClass("meanclose")) {
    //         $('.scrollup').removeClass('back-top');
    //    }
    //});

    /*--------------------------------------
    Isotope initialization
    --------------------------------------*/
    var $container = $(".isotope-wrap");
    if ($container.length > 0) {
        var $isotope;
        var blogGallerIso = $(".featuredContainer", $container).imagesLoaded(function () {
            $isotope = $(".featuredContainer", $container).isotope({
                filter: "*",
                transitionDuration: "1s",
                hiddenStyle: {
                    opacity: 0,
                    transform: "scale(0.001)"
                },
                visibleStyle: {
                    transform: "scale(1)",
                    opacity: 1
                }
            });
        });
        $container.find(".isotope-classes-tab").on("click", "a", function () {
            var $this = $(this);
            $this
                .parent(".isotope-classes-tab")
                .find("a")
                .removeClass("current");
            $this.addClass("current");
            var selector = $this.attr("data-filter");
            $isotope.isotope({
                filter: selector
            });
            return false;
        });
    }

    /*---------------------------------------
    On Click Section Switch
    --------------------------------------- */
    $('[data-type="section-switch"]').on('click', function () {
        if (location.pathname.replace(/^\//, '') === this.pathname.replace(/^\//, '') && location.hostname === this.hostname) {
            var target = $(this.hash);
            if (target.length > 0) {

                target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
                $('html,body').animate({
                    scrollTop: target.offset().top
                }, 1000);
                return false;
            }
        }
    });

    /*-------------------------------------
    Jquery Serch Box
    -------------------------------------*/
    $('a[href="#header-search"]').on("click", function (event) {
        event.preventDefault();
        var target = $("#header-search");
        target.addClass("open");
        setTimeout(function () {
            target.find('input').focus();
        }, 600);
        return false;
    });

    $("#header-search, #header-search button.close").on("click keyup", function (event) {
        if (
            event.target === this ||
            event.target.className === "close" ||
            event.keyCode === 27
        ) {
            $(this).removeClass("open");
        }
    });


    /*-------------------------------------
    Offcanvas Menu activation code
    -------------------------------------*/
    $('#wrapper').on('click', '.offcanvas-menu-btn', function (e) {
        e.preventDefault();
        var $this = $(this),
            wrapper = $(this).parents('body').find('>#wrapper'),
            wrapMask = $('<div />').addClass('offcanvas-mask'),
            offCancas = $('#offcanvas-wrap'),
            position = offCancas.data('position') || 'left';

        if ($this.hasClass('menu-status-open')) {
            wrapper.addClass('open').append(wrapMask);
            $this.removeClass('menu-status-open').addClass('menu-status-close');
            offCancas.css({
                'transform': 'translateX(0)'
            });
        } else {
            removeOffcanvas();
        }

        function removeOffcanvas() {
            wrapper.removeClass('open').find('> .offcanvas-mask').remove();
            $this.removeClass('menu-status-close').addClass('menu-status-open');
            if (position === 'left') {
                offCancas.css({
                    'transform': 'translateX(100%)'
                });
            } else {
                offCancas.css({
                    'transform': 'translateX(-100%)'
                });
            }
        }
        $(".offcanvas-mask, .offcanvas-close").on('click', function () {
            removeOffcanvas();
        });

        return false;
    });

    /*-------------------------------------
    On Scroll 
    -------------------------------------*/
    $(window).on('scroll', function () {

        // Back Top Button
        if ($(window).scrollTop() > 500) {
            $('.scrollup').addClass('back-top');
        } else {
            $('.scrollup').removeClass('back-top');
        }
        // Sticky Header
        if ($('body').hasClass('sticky-header')) {
            var stickyPlaceHolder = $("#rt-sticky-placeholder"),
                menu = $("#header-menu"),
                menuH = menu.outerHeight(),
                topHeaderH = $('#header-topbar').outerHeight() || 0,
                middleHeaderH = $('#header-middlebar').outerHeight() || 0,
                targrtScroll = topHeaderH + middleHeaderH;
            if ($(window).scrollTop() > targrtScroll) {
                menu.addClass('rt-sticky');
                stickyPlaceHolder.height(menuH);
            } else {
                menu.removeClass('rt-sticky');
                stickyPlaceHolder.height(0);
            }
        }
    });

    /*-------------------------------------
    MeanMenu activation code
    --------------------------------------*/
    if ($.fn.meanmenu) {
        $('nav#dropdown').meanmenu({
            siteLogo: "<div class='mobile-menu-nav-back'><a class='logo-mobile' href='/'><img src='/images/logo-mobile.png' alt='logo' <img loading='lazy' class='img-fluid mobile-self-logo mr-1'/></a></div>"
        });
    }
    //Mobile Menu
    $(".meanmenu-reveal").on("click", function () {
        if ($(".meanmenu-reveal").hasClass("meanclose")) {
            $('.scrollup').removeClass('back-top');
        }
        else {
            if ($(window).scrollTop() > 500) {
                $('.scrollup').addClass('back-top');
            } else {
                $('.scrollup').removeClass('back-top');
            }
        }
    });
    //ShortLink Copy
    $("#testcopy").on("click", function () {
        var shortLink = $('#copylinkText');
        var textArea = document.createElement("textarea");
        textArea.value = shortLink.text();
        document.body.appendChild(textArea);
        textArea.select();
        document.execCommand("Copy");
        textArea.remove();
        $("#textcopydiv").removeAttr('hidden');
        $("#textcopydiv").fadeIn(1500);
        $("#textcopydiv").fadeOut(7000);
    });
    /*-------------------------------------
        Google Map
    -------------------------------------*/
    if ($("#googleMap").length) {
        window.onload = function () {
            var styles = [{
                featureType: 'water',
                elementType: 'geometry.fill',
                stylers: [{
                    color: '#b7d0ea'
                }]
            }, {
                featureType: 'road',
                elementType: 'labels.text.fill',
                stylers: [{
                    visibility: 'off'
                }]
            }, {
                featureType: 'road',
                elementType: 'geometry.stroke',
                stylers: [{
                    visibility: 'off'
                }]
            }, {
                featureType: 'road.highway',
                elementType: 'geometry',
                stylers: [{
                    color: '#c2c2aa'
                }]
            }, {
                featureType: 'poi.park',
                elementType: 'geometry',
                stylers: [{
                    color: '#b6d1b0'
                }]
            }, {
                featureType: 'poi.park',
                elementType: 'labels.text.fill',
                stylers: [{
                    color: '#6b9a76'
                }]
            }];
            var options = {
                mapTypeControlOptions: {
                    mapTypeIds: ['Styled']
                },
                center: new google.maps.LatLng(-37.81618, 144.95692),
                zoom: 10,
                disableDefaultUI: true,
                mapTypeId: 'Styled'
            };
            var div = document.getElementById('googleMap');
            var map = new google.maps.Map(div, options);
            var styledMapType = new google.maps.StyledMapType(styles, {
                name: 'Styled'
            });
            map.mapTypes.set('Styled', styledMapType);

            var marker = new google.maps.Marker({
                position: map.getCenter(),
                animation: google.maps.Animation.BOUNCE,
                icon: 'img/map-marker.png',
                map: map
            });
        };
    }


    $(function () {

        /*-------------------------------------
            Doughnut Chart 
        -------------------------------------*/
        if ($("#traffic-doughnut-chart").length) {
            var doughnutChartData = {
                labels: ["Traffics", "Traffics", "Traffics", "Traffics"],
                datasets: [{
                    backgroundColor: ["#413684", "#de2c39", "#f6912c", "#179fb3"],
                    data: [48, 10, 25, 17],
                    borderWidth: [0, 0, 0, 0],
                    label: "Web Traffics"
                },]
            };
            var doughnutChartOptions = {
                responsive: true,
                maintainAspectRatio: false,
                cutoutPercentage: 55,
                rotation: -2.2,
                animation: {
                    duration: 2000
                },
                legend: {
                    display: false
                },
                tooltips: {
                    enabled: true
                },
            }
            var trafficCanvas = $("#traffic-doughnut-chart").get(0).getContext("2d");
            var trafficChart = new Chart(trafficCanvas, {
                type: 'doughnut',
                data: doughnutChartData,
                options: doughnutChartOptions
            });
        }

        /*-------------------------------------
            Slick Slider 
        -------------------------------------*/
        if ($.fn.slick) {
            $('.slick-carousel').each(function () {
                let $carousel = $(this);
                $carousel.imagesLoaded(function () {
                    var data = $carousel.data('slick'),
                        slidesToShowTab = data.slidesToShowTab,
                        slidesToShowMobile = data.slidesToShowMobile;
                    $carousel.slick({
                        centerPadding: '0px',
                        responsive: [{
                            breakpoint: 992,
                            settings: {
                                slidesToShow: slidesToShowTab,
                                slidesToScroll: slidesToShowTab
                            }
                        },
                        {
                            breakpoint: 767,
                            settings: {
                                slidesToShow: slidesToShowMobile,
                                slidesToScroll: slidesToShowMobile
                            }
                        }
                        ]
                    });
                });
            });
        }
        /*-------------------------------------
            My Script
            -------------------------------------*/
        $(".showsuccess").fadeOut(5000);
        $(".meanmenu-reveal").click(function () {
            if ($(".meanmenu-reveal").hasClass("meanclose")) {
                $(".mean-nav ul:first").slideDown(1000);
                $(".mean-nav ul:first").show();
            }
            else {
                $(".mean-nav ul:first").slideUp(1000);
                $(".mean-nav ul:first").hide();
            }
        });

        $(window).scroll(function () {
            if ($(".meanmenu-reveal").hasClass("meanclose")) {
                if ($(window).scrollTop() > 10) {
                    $('.scrollup').removeClass('back-top');
                    $(".meanmenu-reveal").removeClass("meanclose");
                    $(".meanmenu-reveal").html("");
                    $(".meanmenu-reveal").append("<span></span><span></span><span></span>");
                    $(".mean-nav ul:first").slideUp();
                }
            }
        });
        /*-------------------------------------
            Carousel slider initiation
            -------------------------------------*/
        $(".rc-carousel").each(function () {
            var carousel = $(this),
                loop = carousel.data("loop"),
                Canimate = carousel.data("animate"),
                items = carousel.data("items"),
                margin = carousel.data("margin"),
                stagePadding = carousel.data("stage-padding"),
                autoplay = carousel.data("autoplay"),
                autoplayTimeout = carousel.data("autoplay-timeout"),
                smartSpeed = carousel.data("smart-speed"),
                dots = carousel.data("dots"),
                nav = carousel.data("nav"),
                navSpeed = carousel.data("nav-speed"),
                rXsmall = carousel.data("r-x-small"),
                rXsmallNav = carousel.data("r-x-small-nav"),
                rXsmallDots = carousel.data("r-x-small-dots"),
                rXmedium = carousel.data("r-x-medium"),
                rXmediumNav = carousel.data("r-x-medium-nav"),
                rXmediumDots = carousel.data("r-x-medium-dots"),
                rSmall = carousel.data("r-small"),
                rSmallNav = carousel.data("r-small-nav"),
                rSmallDots = carousel.data("r-small-dots"),
                rMedium = carousel.data("r-medium"),
                rMediumNav = carousel.data("r-medium-nav"),
                rMediumDots = carousel.data("r-medium-dots"),
                rLarge = carousel.data("r-large"),
                rLargeNav = carousel.data("r-large-nav"),
                rLargeDots = carousel.data("r-large-dots"),
                rExtraLarge = carousel.data("r-extra-large"),
                rExtraLargeNav = carousel.data("r-extra-large-nav"),
                rExtraLargeDots = carousel.data("r-extra-large-dots"),
                center = carousel.data("center"),
                custom_nav = carousel.data("custom-nav") || "";
            carousel.addClass('owl-carousel');
            var owl = carousel.owlCarousel({
                loop: loop ? true : false,
                animateOut: Canimate,
                items: items ? items : 1,
                lazyLoad: true,
                margin: margin ? margin : 0,
                autoplay: autoplay ? true : false,
                autoplayTimeout: autoplayTimeout ? autoplayTimeout : 1000,
                smartSpeed: smartSpeed ? smartSpeed : 250,
                dots: dots ? true : false,
                nav: nav ? true : false,
                navText: [
                    '<i class="fa fa-angle-right" aria-hidden="true"></i>',
                    '<i class="fa fa-angle-left" aria-hidden="true"></i>'
                ],
                navSpeed: navSpeed ? true : false,
                center: center ? true : false,
                responsiveClass: true,
                responsive: {
                    0: {
                        items: rXsmall ? rXsmall : 1,
                        nav: rXsmallNav ? true : false,
                        dots: rXsmallDots ? true : false
                    },
                    576: {
                        items: rXmedium ? rXmedium : 2,
                        nav: rXmediumNav ? true : false,
                        dots: rXmediumDots ? true : false
                    },
                    768: {
                        items: rSmall ? rSmall : 3,
                        nav: rSmallNav ? true : false,
                        dots: rSmallDots ? true : false
                    },
                    992: {
                        items: rMedium ? rMedium : 4,
                        nav: rMediumNav ? true : false,
                        dots: rMediumDots ? true : false
                    },
                    1200: {
                        items: rLarge ? rLarge : 5,
                        nav: rLargeNav ? true : false,
                        dots: rLargeDots ? true : false
                    },
                    1240: {
                        items: rExtraLarge ? rExtraLarge : 5,
                        nav: rExtraLargeNav ? true : false,
                        dots: rExtraLargeDots ? true : false
                    }
                },
            });

            if (custom_nav) {
                var nav = $(custom_nav),
                    nav_next = $(".rt-next", nav),
                    nav_prev = $(".rt-prev", nav);

                nav_next.on("click", function (e) {
                    e.preventDefault();
                    owl.trigger('next.owl.carousel');
                    return false;
                });

                nav_prev.on("click", function (e) {
                    e.preventDefault();
                    owl.trigger('prev.owl.carousel');
                    return false;
                });
            }
        });
    });
})(jQuery);
