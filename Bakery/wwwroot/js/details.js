$("#edit-button").on("click", () => {
  $("#details-div").slideUp();
  $("#edit-div").slideDown();
});

$("#cancel-edit-button").on("click", () => {
  $("#delete-confirm-div").slideUp();
  $("#edit-div").slideUp();
  $("#details-div").slideDown();
  $("#delete-button").prop("disabled", false);
});

$("#delete-button").on("click", () => {
  $("#delete-confirm-div").slideDown();
  $("#delete-button").prop("disabled", true);
});

$("#cancel-delete-button").on("click", () => {
  $("#delete-confirm-div").slideUp();
  $("#delete-button").prop("disabled", false);
});