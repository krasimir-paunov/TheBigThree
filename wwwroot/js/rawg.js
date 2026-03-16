document.addEventListener('DOMContentLoaded', function () {

    //AUTOCOMPLETE 
    const titleInputs = document.querySelectorAll('.game-title-input');

    titleInputs.forEach(function (input) {
        const wrapper = input.parentElement;
        wrapper.style.position = 'relative';

        const dropdown = document.createElement('div');
        dropdown.className = 'rawg-dropdown';
        wrapper.appendChild(dropdown);

        let debounceTimer;

        input.addEventListener('input', function () {
            clearTimeout(debounceTimer);
            const query = input.value.trim();

            if (query.length < 2) {
                dropdown.innerHTML = '';
                dropdown.style.display = 'none';
                return;
            }

            debounceTimer = setTimeout(async function () {
                try {
                    const response = await fetch(`/rawg/search?query=${encodeURIComponent(query)}`);
                    const games = await response.json();

                    dropdown.innerHTML = '';

                    if (!games.length) {
                        dropdown.style.display = 'none';
                        return;
                    }

                    games.forEach(function (game) {
                        const item = document.createElement('div');
                        item.className = 'rawg-dropdown-item';
                        item.innerHTML = `
                            <img src="${game.backgroundImage || '/images/placeholder.jpg'}" alt="${game.name}" />
                            <div class="rawg-dropdown-info">
                                <span class="rawg-dropdown-name">${game.name}</span>
                                <span class="rawg-dropdown-meta">${game.released ? game.released.substring(0, 4) : ''} ${game.rating ? '⭐ ' + game.rating.toFixed(1) : ''}</span>
                            </div>
                        `;

                        item.addEventListener('click', function () {
                            input.value = game.name;

                            const container = input.closest('.game-slot-container');
                            if (container) {
                                const imageInput = container.querySelector('.game-image-input');
                                if (imageInput && game.backgroundImage) {
                                    imageInput.value = game.backgroundImage;
                                }

                                if (game.genres && game.genres.length > 0) {
                                    const firstGenre = game.genres[0].name.toLowerCase();
                                    const genreSelect = container.querySelector('select');
                                    if (genreSelect) {
                                        for (let option of genreSelect.options) {
                                            if (option.text.toLowerCase() === firstGenre) {
                                                genreSelect.value = option.value;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                            dropdown.innerHTML = '';
                            dropdown.style.display = 'none';
                        });

                        dropdown.appendChild(item);
                    });

                    dropdown.style.display = 'block';
                } catch (e) {
                    dropdown.style.display = 'none';
                }
            }, 350);
        });

        document.addEventListener('click', function (e) {
            if (!wrapper.contains(e.target)) {
                dropdown.innerHTML = '';
                dropdown.style.display = 'none';
            }
        });
    });

    //GAME DETAILS MODAL
    const gameCovers = document.querySelectorAll('.game-cover-clickable');

    let screenshotUrls = [];
    let currentScreenshot = 0;

    function openLightbox(urls, index) {
        currentScreenshot = index;
        document.getElementById('rawgLightboxImg').src = urls[currentScreenshot];
        document.getElementById('lightboxCounter').textContent = `${currentScreenshot + 1} / ${urls.length}`;

        const prevBtn = document.getElementById('lightboxPrev');
        const nextBtn = document.getElementById('lightboxNext');

        prevBtn.onclick = function () {
            currentScreenshot = (currentScreenshot - 1 + urls.length) % urls.length;
            document.getElementById('rawgLightboxImg').src = urls[currentScreenshot];
            document.getElementById('lightboxCounter').textContent = `${currentScreenshot + 1} / ${urls.length}`;
        };

        nextBtn.onclick = function () {
            currentScreenshot = (currentScreenshot + 1) % urls.length;
            document.getElementById('rawgLightboxImg').src = urls[currentScreenshot];
            document.getElementById('lightboxCounter').textContent = `${currentScreenshot + 1} / ${urls.length}`;
        };

        const lightbox = new bootstrap.Modal(document.getElementById('rawgLightbox'));
        lightbox.show();
    }

    gameCovers.forEach(function (cover) {
        cover.addEventListener('click', async function () {
            const gameName = cover.dataset.gameName;
            if (!gameName) return;

            const modal = document.getElementById('rawgGameModal');
            const modalContent = document.getElementById('rawgModalContent');
            const modalLoading = document.getElementById('rawgModalLoading');

            if (!modal) return;

            modalContent.style.display = 'none';
            modalLoading.style.display = 'flex';

            const bsModal = new bootstrap.Modal(modal);
            bsModal.show();

            try {
                const response = await fetch(`/rawg/gamedetails?gameName=${encodeURIComponent(gameName)}`);
                const data = await response.json();

                if (data.error) {
                    const bsModalInstance = bootstrap.Modal.getInstance(modal);
                    if (bsModalInstance) bsModalInstance.hide();
                    return;
                }

                document.getElementById('rawgGameTitle').textContent = data.name;
                document.getElementById('rawgGameBg').style.backgroundImage = `url('${data.backgroundImage}')`;
                document.getElementById('rawgMetacritic').textContent = data.metacriticScore ?? 'N/A';
                document.getElementById('rawgReleased').textContent = data.released || 'N/A';
                document.getElementById('rawgDeveloper').textContent = data.developers || 'N/A';
                document.getElementById('rawgPlatforms').textContent = data.platforms || 'N/A';
                document.getElementById('rawgGenres').textContent = data.genres || 'N/A';

                const screenshotsContainer = document.getElementById('rawgScreenshots');
                screenshotsContainer.innerHTML = '';
                screenshotUrls = [];

                if (data.screenshots && data.screenshots.length) {
                    screenshotUrls = data.screenshots;

                    data.screenshots.forEach(function (url, index) {
                        const img = document.createElement('img');
                        img.src = url;
                        img.className = 'rawg-screenshot';
                        img.alt = 'Screenshot';
                        img.addEventListener('click', function () {
                            openLightbox(screenshotUrls, index);
                        });
                        screenshotsContainer.appendChild(img);
                    });
                }

                modalLoading.style.display = 'none';
                modalContent.style.display = 'block';

            } catch (e) {
                modalLoading.innerHTML = '<p class="text-white-50">Failed to load game data.</p>';
            }
        });
    });
});