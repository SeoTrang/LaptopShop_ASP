
function displayImage(input) {
    const uploadedImage = document.getElementById('uploadedImage_avatar');
    const label = input.parentElement;

    if (input.files && input.files[0]) {
        const reader = new FileReader();

        reader.onload = function(e) {
            uploadedImage.src = e.target.result;
            label.querySelector('i').style.display = 'none';
            label.querySelector('span').style.display = 'none';
        };

        reader.readAsDataURL(input.files[0]);
    }
}

function displayListImage(input) {
    var files = input.files;
    var selectedImagesContainer = document.getElementById("selectedImagesContainer");
  
    const label = input.parentElement;
    // Ẩn thẻ i và thẻ span
    // document.querySelector(".box-image label i, .box-image label span").style.display = "none";
  
    // Hiển thị danh sách ảnh lên thẻ selectedImagesContainer
    for (var i = 0; i < files.length; i++) {
      var file = files[i];
      var image = document.createElement("img");
      image.src = URL.createObjectURL(file);
      image.alt = file.name;
      selectedImagesContainer.appendChild(image);
    }
    label.querySelector('i').style.display = 'none';
    label.querySelector('span').style.display = 'none';
  }

  tinymce.init({
    selector: 'textarea',
    plugins: 'ai tinycomments mentions anchor autolink charmap codesample emoticons image link lists media searchreplace table visualblocks wordcount checklist mediaembed casechange export formatpainter pageembed permanentpen footnotes advtemplate advtable advcode editimage tableofcontents mergetags powerpaste tinymcespellchecker autocorrect a11ychecker typography inlinecss',
    toolbar: 'undo redo | blocks fontfamily fontsize | bold italic underline strikethrough | link image media table mergetags | align lineheight | tinycomments | checklist numlist bullist indent outdent | emoticons charmap | removeformat',
    tinycomments_mode: 'embedded',
    tinycomments_author: 'Author name',
    mergetags_list: [
      { value: 'First.Name', title: 'First Name' },
      { value: 'Email', title: 'Email' },
    ],
    ai_request: (request, respondWith) => respondWith.string(() => Promise.reject("See docs to implement AI Assistant"))
  });