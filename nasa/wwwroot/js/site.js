// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $(document).on("click", ".open-controlRover", function () {
        var roverId = $(this).data('id');
        var roverXpos = $(this).data('xpos');
        var roverYpos = $(this).data('ypos');
        var roverHeading = $(this).data('heading');

        $(".modal-body #roverId").html(roverId);
        $(".modal-body #roverXpos").html(roverXpos);
        $(".modal-body #roverYpos").html(roverYpos);
        $(".modal-body #roverHeading").html(roverHeading);
        $("#roverMovements").val('');
    });

    $(document).on("click", ".addMovement", function () {
        var roverDirection = $(this).data('direction');
        var currentMovements = $("#roverMovements").val();
        currentMovements += roverDirection;

        $("#roverMovements").val(currentMovements);
    });

    $(document).on("click", "#sendMovements", function () {
        var roverId = $("#roverId").html();
        var currentMovements = $("#roverMovements").val();

        if (currentMovements.length > 0) {
            var data = {
                "move": currentMovements
            }

            $.ajax({
                url: `https://localhost:5003/api/rovers/${roverId}/move`,
                data: JSON.stringify(data),
                type: 'PATCH',
                contentType: 'application/json',
                dataType: 'json',
                processData: false,
                success: function (data) {
                    console.log('success');
                    $('#controlRover').modal('hide');
                },
                error: function () {
                    console.log('Error');
                },
            });
        }
    });

    $(document).on("click", "#launchRover", function () {
        var roverH = $("#newRoverHeading").val();
        var roverX = $("#newRoverX").val();
        var roverY = $("#newRoverY").val();

        if (roverX && roverY) {
            var data = {
                "XPos": parseInt(roverX),
                "YPos": parseInt(roverY),
                "Heading": roverH,
            }

            $.ajax({
                url: `https://localhost:5003/api/rovers`,
                data: JSON.stringify(data),
                type: 'POST',
                contentType: 'application/json',
                dataType: 'json',
                success: function (data) {
                    console.log('Rover Deployed');
                    $('#newRover').modal('hide');
                },
                error: function () {
                    console.log('Error');
                },
            });
        }
    });

    $(document).on("click", "#newGrid", function () {
        var grid = $("#gridSize").val();

        var data = {
            "maxSize": parseInt(grid)
        }

        $.ajax({
            url: `https://localhost:5003/api/grid`,
            data: JSON.stringify(data),
            type: 'PUT',
            contentType: 'application/json',
            dataType: 'json',
            success: function (data) {
                console.log('Grid Created');
                $('#newRover').modal('hide');
                location.reload();
            },
            error: function () {
                console.log('Error');
            },
        });
    });

});