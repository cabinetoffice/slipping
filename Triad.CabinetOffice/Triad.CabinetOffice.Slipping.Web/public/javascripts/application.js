/* global $ */
/* global GOVUK */

// Warn about using the kit in production
if (window.console && window.console.info) {
  window.console.info('GOV.UK Prototype Kit - do not use for production')
}

$(document).ready(function () {
  // Use GOV.UK shim-links-with-button-role.js to trigger a link styled to look like a button,
  // with role="button" when the space key is pressed.
  GOVUK.shimLinksWithButtonRole.init()

  // Show and hide toggled content
  // Where .multiple-choice uses the data-target attribute
  // to toggle hidden content
  var showHideContent = new GOVUK.ShowHideContent()
  showHideContent.init()
})

    $(document).on('click', '.button-add-another', function (e) {
        var beforeThis = $(this).closest('.grid-row');
        e.preventDefault();
        var clone = $('.opposition-mp-item').first().clone();
        var index = $('.opposition-mp-item').length;
        $('h2', clone).text('MP ' + (index + 1));
        $('input.form-control', clone).val('');
        $('input[type="hidden"]', clone).val(0);
        $("#opposition-mp-list").append(clone);
        sortFields();
    });

    $(document).on('click', '.remove-list-item', function (e) {
      e.preventDefault();
      $(this).closest('.list-item-wrapper').remove();
      sortFields();
    });

    function sortFields() {
      var listCounter = 1;
      var inputCounter = 0;

      $(document).find('.list-item-wrapper').each(function () {
        $(this).find('h2').text('MP ' + listCounter);

        if ($(this).find('.remove-list-item').length === 0) {
          $(this).find('.list-item:last').append('<a id="remove-item-' + listCounter + '" class="remove-list-item" href="#">Remove this</a>');
        } else {
          $(this).find('.remove-list-item').attr('id', 'remove-item-' + listCounter);
        }

        $(this).find('.list-item').children('label').each(function () {
            $(this).attr('for', $(this).attr('for').replace(/MPs\_[0-9]*\_/, 'MPs_' + inputCounter + '_'));
            inputCounter++;
        });

        $(this).find('.list-item').children('input').each(function () {
          var labelNo = $(this).parent().find('label').attr('for').split('_')[1];
          $(this).attr('id', $(this).attr('id').replace(/MPs\_[0-9]*\_/, 'MPs_' + labelNo + '_'));
          $(this).attr('name', $(this).attr('name').replace(/MPs\[[0-9]*\]/, 'MPs[' + labelNo + ']'));
          
        });

        listCounter++;
      });

      if ($(document).find('.list-item-wrapper').length === 1) {
        $('.remove-list-item').remove();
      }
    }

    var charCount = new GOVUK.CharCount()
charCount.init({
  selector: '.js-word-count',
  wordCount: true
})