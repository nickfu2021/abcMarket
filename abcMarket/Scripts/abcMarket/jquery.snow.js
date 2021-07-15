/**
 * jquery.snow - jQuery Snow Effect Plugin
 *
 * Available under MIT licence
 *
 * @version 1 (21. Jan 2012)
 * @author Ivan Lazarevic
 * @requires jQuery
 * @see http://workshop.rs
 *
 * @params minSize - min size of snowflake, 10 by default
 * @params maxSize - max size of snowflake, 20 by default
 * @params newOn - frequency in ms of appearing of new snowflake, 500 by default
 * @params flakeColor - color of snowflake, #FFFFFF by default
 * @example $.fn.snow({ maxSize: 200, newOn: 1000 });
 */

(function($){
	 var interval;

	$.fn.snow = function(options){
	
			var $flake 			= $('<div id="flake" />').css({'position': 'absolute', 'top': '-50px'}).html('*'),
				documentHeight 	= $(document).height(),
				documentWidth	= $(document).width(),
				defaults		= {
									minSize		: 10,
									maxSize		: 15,
									newOn		: 1000,
									flakeColor	: "white"
								},
				options			= $.extend({}, defaults, options);
				
			
			 interval= setInterval( function(){
				var startPositionLeft 	= Math.random() * documentWidth - 100,
				 	startOpacity		= 1.0,
					sizeFlake			= options.minSize + Math.random() * options.maxSize,
					endPositionTop		= documentHeight - 40,
					endPositionLeft		= startPositionLeft - 100 + Math.random() * 200,
					durationFall		= documentHeight * 10 + Math.random() * 5000;
				$flake
					.clone()
					.appendTo('body')
					.css(
						{
							left: startPositionLeft,
							opacity: startOpacity,
							'font-size': sizeFlake,
							color: options.flakeColor
						}
					)
					.animate(
						{
							top: endPositionTop,
							left: endPositionLeft,
							opacity: 1.0
						},
						durationFall,
						'linear',
						function() {
							$(this).remove()
						}
					);
			}, options.newOn);
	
	};
	
	$.fn.stopsnow = function(){
        clearInterval(interval);
	}
})(jQuery);