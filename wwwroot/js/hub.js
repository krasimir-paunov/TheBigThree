document.addEventListener('DOMContentLoaded', function () {
    const searchInput = document.querySelector('input[name="SearchTerm"]');
    const genreSelect = document.querySelector('select[name="GenreFilter"]');
    const clearBtn = document.getElementById('clearBtn');

    function updateClearButton() {
        const searchEmpty = searchInput.value.trim() === '';
        const genreDefault = genreSelect.value === '';

        if (searchEmpty && genreDefault) {
            clearBtn.classList.add('disabled');
            clearBtn.setAttribute('aria-disabled', 'true');
            clearBtn.tabIndex = -1;
        } else {
            clearBtn.classList.remove('disabled');
            clearBtn.removeAttribute('aria-disabled');
            clearBtn.tabIndex = 0;
        }
    }

    searchInput.addEventListener('input', updateClearButton);
    genreSelect.addEventListener('change', updateClearButton);

    updateClearButton();
});