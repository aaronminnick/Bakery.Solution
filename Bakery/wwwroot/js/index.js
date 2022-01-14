$("li").filter(function() {
  return this.id.match(/^T/);
}).hover(
  function() {
    $(this).addClass("match-origin");
    let targets = $(this).data("flavortreats").toString().split("");
    targets.forEach((target) => {
      $(`#F${target}`).addClass("match-target");
    });
  },
  function() {
    $(this).removeClass("match-origin");
    let targets = $(this).data("flavortreats").toString().split("");
    console.log(targets);
    targets.forEach((target) => {
      $(`#F${target}`).removeClass("match-target");
    });
  }
);

$("li").filter(function() {
  return this.id.match(/^F/);
}).hover(
  function() {
    $(this).addClass("match-origin");
    let targets = $(this).data("flavortreats").toString().split("");
    targets.forEach((target) => {
      $(`#T${target}`).addClass("match-target");
    });
  },
  function() {
    $(this).removeClass("match-origin");
    let targets = $(this).data("flavortreats").toString().split("");
    console.log(targets);
    targets.forEach((target) => {
      $(`#T${target}`).removeClass("match-target");
    });
  }
);