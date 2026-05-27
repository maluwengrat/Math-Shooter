mergeInto(LibraryManager.library, {
  FocusCanvas: function () {
    var canvas = document.querySelector("#unity-canvas");
    if (canvas) canvas.focus();
  }
});