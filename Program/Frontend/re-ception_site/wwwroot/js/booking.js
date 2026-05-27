// wwwroot/js/booking.js

window.bookingFunctions = {
    // Store room data globally for the popup
    currentRoomData: null,
    
    // Initialize the booking button with room data
    initBookingButton: function(roomData) {
        console.log("Initializing booking button with:", roomData);
        window.bookingFunctions.currentRoomData = roomData;
        
        const bookBtn = document.getElementById('bookNowButton');
        if (bookBtn) {
            // Remove existing listeners to avoid duplicates
            const newBtn = bookBtn.cloneNode(true);
            bookBtn.parentNode.replaceChild(newBtn, bookBtn);
            
            newBtn.addEventListener('click', function() {
                window.bookingFunctions.openBookingPopup();
            });
            console.log("Booking button listener attached");
        } else {
            console.log("Book button not found, will retry in 500ms");
            setTimeout(() => window.bookingFunctions.initBookingButton(roomData), 500);
        }
    },
    
    // Open the booking popup
    openBookingPopup: function() {
        const roomData = window.bookingFunctions.currentRoomData;
        if (!roomData) {
            console.error("No room data available");
            return;
        }
        
        // Check if modal already exists
        const existingModal = document.getElementById('bookingModalOverlay');
        if (existingModal) {
            existingModal.remove();
        }
        
        // Escape HTML helper
        function escapeHtml(str) {
            if (!str) return '';
            return String(str).replace(/[&<>]/g, function(m) {
                if (m === '&') return '&amp;';
                if (m === '<') return '&lt;';
                if (m === '>') return '&gt;';
                return m;
            });
        }
        
        // Create modal HTML
        const modalHTML = `
            <div id="bookingModalOverlay" class="modal-overlay">
                <div class="booking-modal">
                    <div class="modal-header">
                        <h2>🌸 Book Your Stay</h2>
                        <button class="close-modal" id="closeModalBtn">&times;</button>
                    </div>
                    <div class="room-preview">
                        <p><strong>${escapeHtml(roomData.roomNumber || roomData.number || 'Selected Room')}</strong> · ${escapeHtml(roomData.roomType || roomData.type || 'Deluxe Suite')}</p>
                        <p style="font-size:0.8rem; margin-top:0.3rem;">✨ Price: $${roomData.pricePerNight || roomData.price || 299}/night</p>
                    </div>
                    <form id="bookingForm">
                        <div class="form-group">
                            <label>👥 Number of guests</label>
                            <select id="guestCount" required>
                                <option value="1">1 guest</option>
                                <option value="2" selected>2 guests</option>
                                <option value="3">3 guests</option>
                                <option value="4">4 guests</option>
                                <option value="5">5 guests</option>
                                <option value="6">6 guests</option>
                            </select>
                        </div>
                        
                        <div class="form-row">
                            <div class="form-group">
                                <label>📅 Date from (check-in)</label>
                                <input type="date" id="dateFrom" required>
                            </div>
                            <div class="form-group">
                                <label>📅 Date to (check-out)</label>
                                <input type="date" id="dateTo" required>
                            </div>
                        </div>
                        
                        <button type="submit" class="submit-booking">✨ Confirm Booking</button>
                    </form>
                </div>
            </div>
        `;
        
        document.body.insertAdjacentHTML('beforeend', modalHTML);
        
        const modalOverlay = document.getElementById('bookingModalOverlay');
        const closeBtn = document.getElementById('closeModalBtn');
        const bookingForm = document.getElementById('bookingForm');
        
        // Set default dates
        const today = new Date();
        const tomorrow = new Date(today);
        tomorrow.setDate(tomorrow.getDate() + 1);
        
        const dateFromInput = document.getElementById('dateFrom');
        const dateToInput = document.getElementById('dateTo');
        
        if (dateFromInput) {
            dateFromInput.value = today.toISOString().split('T')[0];
            dateFromInput.min = today.toISOString().split('T')[0];
        }
        if (dateToInput) {
            dateToInput.value = tomorrow.toISOString().split('T')[0];
            dateToInput.min = tomorrow.toISOString().split('T')[0];
        }
        
        // Update check-out min date
        if (dateFromInput && dateToInput) {
            dateFromInput.addEventListener('change', function() {
                const newMinDate = new Date(dateFromInput.value);
                newMinDate.setDate(newMinDate.getDate() + 1);
                dateToInput.min = newMinDate.toISOString().split('T')[0];
                if (dateToInput.value < dateToInput.min) {
                    dateToInput.value = dateToInput.min;
                }
            });
        }
        
        // Show modal
        setTimeout(() => {
            modalOverlay.classList.add('active');
        }, 10);
        
        // Close modal function
        function closeModal() {
            modalOverlay.classList.remove('active');
            setTimeout(() => {
                modalOverlay.remove();
            }, 300);
        }
        
        if (closeBtn) {
            closeBtn.addEventListener('click', closeModal);
        }
        
        modalOverlay.addEventListener('click', (e) => {
            if (e.target === modalOverlay) closeModal();
        });
        
        // Handle form submission
        if (bookingForm) {
            bookingForm.addEventListener('submit', (e) => {
                e.preventDefault();
                
                const guests = document.getElementById('guestCount').value;
                const dateFrom = document.getElementById('dateFrom').value;
                const dateTo = document.getElementById('dateTo').value;
                
                if (!dateFrom || !dateTo) {
                    alert('🌸 Please select both check-in and check-out dates.');
                    return;
                }
                
                const checkInDate = new Date(dateFrom);
                const checkOutDate = new Date(dateTo);
                
                if (checkOutDate <= checkInDate) {
                    alert('📅 Check-out date must be after check-in date.');
                    return;
                }
                
                const nights = Math.round((checkOutDate - checkInDate) / (1000 * 60 * 60 * 24));
                const pricePerNight = roomData.pricePerNight || roomData.price || 299;
                const totalPrice = nights * pricePerNight;
                const roomName = roomData.roomNumber || roomData.number || 'Selected Room';
                const roomTypeName = roomData.roomType || roomData.type || 'Suite';
                
                const confirmationMessage = `🎉 Booking Confirmed! 🎉\n━━━━━━━━━━━━━━━━━━━━━\n🏨 Room: ${roomName} (${roomTypeName})\n👥 Guests: ${guests}\n📅 Check-in: ${dateFrom}\n📅 Check-out: ${dateTo}\n⏱️ Stay duration: ${nights} night(s)\n💰 Total: $${totalPrice} ($${pricePerNight}/night)\n━━━━━━━━━━━━━━━━━━━━━\n✨ Thank you for booking with Grand Horizon!`;
                
                alert(confirmationMessage);
                
                // You can also call back to C# here if needed
                if (window.dotNetReference) {
                    window.dotNetReference.invokeMethodAsync('OnBookingConfirmed', {
                        roomId: roomData.id,
                        guests: parseInt(guests),
                        dateFrom: dateFrom,
                        dateTo: dateTo,
                        nights: nights,
                        totalPrice: totalPrice
                    });
                }
                
                closeModal();
            });
        }
    }
};