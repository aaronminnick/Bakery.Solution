$(".remove-ft-button").on("click", function() {
  let treatId = $(this).siblings(".treat-id").val();
  let flavorId = $(this).siblings(".flavor-id").val();
  $("#flavor-treats-div").load("../RemoveFlavorTreat", {TreatId: treatId, FlavorId: flavorId});
});

$(".add-ft-button").on("click", function() {
  let treatId = $(this).siblings(".treat-id").val();
  let flavorId = $(this).siblings(".flavor-id").val();
  $("#flavor-treats-div").load("../AddFlavorTreat", {TreatId: treatId, FlavorId: flavorId});
});