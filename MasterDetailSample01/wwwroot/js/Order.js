// Global Variables
let orders = [];
let products = [];
let isEditMode = true;

//Create Order Form Elements 
const customerId = document.querySelector("#customerId");
const customerFirstName = document.querySelector("#customerFirstName");
const customerLastName = document.querySelector("#customerLastName");
const sellerId = document.querySelector("#sellerId");
const sellerFirstName = document.querySelector("#sellerFirstName");
const sellerLastName = document.querySelector("#sellerLastName");
const customerTableBody = document.querySelector("#customerTableBody");
const sellerTableBody = document.querySelector("#sellerTableBody");
const addDetailBtn = document.querySelector("#addDetailBtn");
const createBtn = document.querySelector(".create-btn");
const detailsTableBody = document.querySelector("#detailsTable tbody");

//Edit Order Form Elements
const editOrderId = document.querySelector("#editOrderId");
const editOrderGuidKey = document.querySelector("#editOrderGuidKey");
const editCustomerId = document.querySelector("#editCustomerId");
const editCustomerFirstName = document.querySelector("#editCustomerFirstName");
const editCustomerLastName = document.querySelector("#editCustomerLastName");
const editSellerId = document.querySelector("#editSellerId");
const editSellerFirstName = document.querySelector("#editSellerFirstName");
const editSellerLastName = document.querySelector("#editSellerLastName");
const editDetailsTableBody = document.querySelector("#editDetailsTableBody");
const editFinalPrice = document.querySelector("#editFinalPrice");
const editAddDetailBtn = document.querySelector("#editAddDetailBtn");
const updateOrderBtn = document.querySelector("#updateOrderBtn");

//Modal Elements
const createOrderModal = document.getElementById("createOrderModal");
const editOrderModal = document.getElementById("editOrderModal");
const customerModal = document.getElementById("customerModal");
const sellerModal = document.getElementById("sellerModal");


// *** Helper & Calculation Functions



// Calculate row total in edit form
function calculateEditRowTotal(row) {
    const unitPrice = Number(row.querySelector(".unit-price").value) || 0;
    const quantity = Number(row.querySelector(".quantity").value) || 0;
    const total = unitPrice * quantity;
    row.querySelector(".total-price").value = total.toFixed(2);
    calculateEditFinalPrice();  // Call the global function
}


// Calculate final total price in edit form
function calculateEditFinalPrice() {
    let total = 0;
    document.querySelectorAll("#editDetailsTableBody tr").forEach(row => {
        total += Number(row.querySelector(".total-price").value) || 0;
    });
    document.getElementById("editFinalPrice").textContent = total.toLocaleString();
}




// Calculate final total price in create form
const calculateFinalPrice = () => {
    let total = 0;
    document.querySelectorAll("#detailsTable tbody tr").forEach(row => {
        total += Number(row.querySelector(".total-price").value) || 0;
    });
    document.getElementById("finalPrice").textContent = total.toLocaleString();
};


//Calculate row total in create form (unit price * quantity)
const calculateRowTotal = (row) => {
    const unitPrice = Number(row.querySelector(".unit-price").value) || 0;
    const quantity = Number(row.querySelector(".quantity").value) || 0;
    row.querySelector(".total-price").value = unitPrice * quantity;
    calculateFinalPrice();
};





//Clear create order form fields
const clearInputs = () => {
    customerId.value = "";
    customerFirstName.value = "";
    customerLastName.value = "";
    sellerId.value = "";
    sellerFirstName.value = "";
    sellerLastName.value = "";
    detailsTableBody.innerHTML = "";
    document.getElementById("finalPrice").textContent = "0";
};

//Validate create order form
const validateOrder = () => {
    if (!customerId.value) {
        return { isValid: false, message: "Please select a customer" };
    }
    if (!sellerId.value) {
        return { isValid: false, message: "Please select a seller" };
    }
    if (document.querySelectorAll("#detailsTable tbody tr").length === 0) {
        return { isValid: false, message: "Add at least one order detail" };
    }
    return { isValid: true };
};




// *** Data Fetching Functions



// Fetch customers list from server
const fetchCustomers = async () => {
    try {
        const response = await fetch("/Customer/GetAll");
        if (!response.ok) throw new Error("Failed to fetch customers");
        const result = await response.json();
        showCustomers(result);
    } catch (error) {
        console.error("Error in fetchCustomers:", error);
    }
};


//Fetch sellers list from server
const fetchSellers = async () => {
    try {
        const response = await fetch("/Seller/GetAll");
        if (!response.ok) throw new Error("Failed to fetch sellers");
        const result = await response.json();
        showSellers(result);
    } catch (error) {
        console.error("Error in fetchSellers:", error);
    }
};


//Fetch products list from server
const fetchProducts = async () => {
    try {
        const response = await fetch("/Product/GetAll");
        if (!response.ok) throw new Error("Failed to fetch products");
        products = await response.json();
    } catch (error) {
        console.error("Error in fetchProducts:", error);
    }
};


//Fetch and display orders list
const loadOrders = async () => {
    try {
        const response = await fetch('/Order/GetAll');
        if (!response.ok) throw new Error("Failed to fetch orders");
        const result = await response.json();
        orders = result;
        renderOrders(result);
    } catch (error) {
        console.error("Error in loadOrders:", error);
    }
};


// *** Rendering Functions



//Render orders list in main table
const renderOrders = (ordersList) => {
    const rows = ordersList.map(order => {
        // Build detail rows
        const detailRows = order.orderDetails.map(detail =>
            `<tr>
                <td class="text-center">${detail.productName}</td>
                <td class="text-center">${detail.quantity}</td>
                <td class="text-center">${detail.unitPrice.toLocaleString()}</td>
                <td class="text-center fw-bold text-info">${detail.totalPrice.toLocaleString()}</td>
            </tr>`
        ).join("");

        // Calculate final price
        const finalPrice = order.orderDetails.reduce(
            (sum, detail) => sum + detail.totalPrice, 0
        );

        // Main row + collapsible detail row
        return `
        <tr class="align-middle">
            <td>
                <div class="text-start fw-bold text-dark">
                    <i class="bi bi-person-circle text-secondary me-1"></i>
                    ${order.customerLastName} ${order.customerFirstName}
                </div>
            </td>
            <td>
                <div class="text-start fw-bold text-dark">
                    <i class="bi bi-person-workspace text-secondary me-1"></i>
                    ${order.sellerFirstName} ${order.sellerLastName}
                </div>
            </td>
            <td class="text-center fw-bold text-success">
                ${order.finalPrice.toLocaleString()}
            </td>
            <td class="text-center">
                <button class="btn btn-outline-secondary btn-sm"
                        data-bs-toggle="collapse"
                        data-bs-target="#detail${order.id}">
                    <i class="bi bi-chevron-down"></i>
                </button>
            </td>
            <td>
                <button class="btn btn-warning edit-order"
                        data-id="${order.id}">
                    Edit Order
                </button>
            </td>
        </tr>
        <tr class="collapse" id="detail${order.id}">
            <td colspan="4" class="bg-light">
                <table class="table table-bordered table-hover mb-0">
                    <thead class="table-info">
                        <tr>
                            <th class="text-center">Product</th>
                            <th class="text-center">Qty</th>
                            <th class="text-center">Unit Price</th>
                            <th class="text-center">Total Price</th>
                        </tr>
                    </thead>
                    <tbody>
                        ${detailRows}
                        <tr class="fw-bold">
                            <td colspan="3" class="text-start text-dark fs-5">
                                Final Price
                            </td>
                            <td class="text-center text-success fs-5">
                                ${finalPrice.toLocaleString()}
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        `;
    }).join("");

    $("#orderTableBody").html(rows);
};


//Display customers list in modal
const showCustomers = (customers) => {
    customerTableBody.innerHTML = "";
    customers.forEach(customer => {
        customerTableBody.insertAdjacentHTML(
            "beforeend",
            `
            <tr>
                <td>${customer.customerLastName}</td>
                <td>${customer.customerFirstName}</td>
                <td>${customer.phoneNumber ?? ""}</td>
                <td>
                    <button class="btn btn-primary select-customer"
                            data-id="${customer.id}"
                            data-name="${customer.customerFirstName}"
                            data-lastname="${customer.customerLastName}">
                        Select
                    </button>
                </td>
            </tr>
            `
        );
    });
};


//Display sellers list in modal
const showSellers = (sellers) => {
    sellerTableBody.innerHTML = "";
    sellers.forEach(seller => {
        sellerTableBody.insertAdjacentHTML(
            "beforeend",
            `
            <tr>
                <td>${seller.sellerFirstName}</td>
                <td>${seller.sellerLastName}</td>
                <td>
                    <button class="btn btn-primary select-seller"
                            data-id="${seller.id}"
                            data-firstname="${seller.sellerFirstName}"
                            data-lastname="${seller.sellerLastName}">
                        Select
                    </button>
                </td>
            </tr>
            `
        );
    });
};




// *** Create Order Functions


// Create new order and send to server
const createOrder = async () => {
    try {
        // Validation
        const validation = validateOrder();
        if (!validation.isValid) {
            alert(validation.message);
            return;
        }

        // Generate unique identifier
        const orderGuid = crypto.randomUUID();

        // Extract details from table
        const orderDetails = [];
        document.querySelectorAll("#detailsTable tbody tr").forEach(row => {
            orderDetails.push({
                parentGuid: orderGuid,
                productId: row.querySelector(".product-id").value,
                unitPrice: Number(row.querySelector(".unit-price").value),
                quantity: Number(row.querySelector(".quantity").value)
            });
        });

        // Build request DTO
        const dto = {
            guidKey: orderGuid,
            customerId: customerId.value,
            sellerId: sellerId.value,
            orderDetails
        };

        // Send to server
        const response = await fetch("/Order/PostOrder", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(dto)
        });

        if (!response.ok) {
            const error = await response.text();
            alert(error);
            return;
        }

        alert("Order created successfully");

        // Close modal and clear form
        bootstrap.Modal.getInstance(createOrderModal).hide();
        clearInputs();
        await loadOrders();

    } catch (error) {
        console.error("Error in createOrder:", error);
        alert("Error creating order");
    }
};


// *** Edit Order Functions


//Fill edit modal with order data

function fillEditModal(order) {
    // Fill header fields
 
    editOrderId.value = order.id;
    editOrderGuidKey.value = order.guidKey;
    editCustomerId.value = order.customerId;
    editCustomerFirstName.value = order.customerFirstName;
    editCustomerLastName.value = order.customerLastName;
    editSellerId.value = order.sellerId;
    editSellerFirstName.value = order.sellerFirstName;
    editSellerLastName.value = order.sellerLastName;



    // Clear details table
    editDetailsTableBody.innerHTML = "";

    

    // Add order details
    order.orderDetails.forEach(detail => {
        addEditDetailRow(detail);
    });

    // Calculate final price
    calculateEditFinalPrice();

    // Show modal
    const modal = bootstrap.Modal.getOrCreateInstance(editOrderModal);
    modal.show();
}


//Add detail row to edit table
function addEditDetailRow(detail) {
    const row = document.createElement("tr");
    row.innerHTML = `
        <td>
            <select class="form-select product-id">
                ${products.map(p => `
                    <option value="${p.id}"
                        ${p.id == detail.productId ? "selected" : ""}>
                        ${p.productName}
                    </option>
                `).join("")}
            </select>
        </td>
        <td>
            <input class="form-control unit-price"
                   value="${detail.unitPrice}"
                   >
        </td>
        <td>
            <input class="form-control quantity"
                   value="${detail.quantity}">
        </td>
        <td>
            <input class="form-control total-price"
                   value="${detail.totalPrice}"
                   readonly>
        </td>
       
        <td>
            <button class="btn btn-danger remove-detail">
                Delete
            </button>
        </td>
    `;
    editDetailsTableBody.appendChild(row);
}




// Update order (send to server)

// Update order (send to server)
const updateOrder = async () => {
    console.log("Starting order update process...");

    try {
        
        // STEP 1: Get Order GUID (همه شناسه‌ها Guid هستند)
       const orderId = document.querySelector("#editOrderId").value;     
        const orderGuidKey = document.querySelector("#editOrderGuidKey").value; 

        console.log("📌 Order ID (Guid):", orderId);
        console.log("📌 Order GuidKey:", orderGuidKey);

        if (!orderId) {
            alert("Order ID not found!");
            return;
        }

        
        // STEP 2: Get Customer and Seller (هر دو Guid هستند)
        const customerId = document.querySelector("#editCustomerId").value;
        const sellerId = document.querySelector("#editSellerId").value;

        console.log("Customer ID (Guid):", customerId);
        console.log("Seller ID (Guid):", sellerId);

        if (!customerId) {
            alert("Please select a customer!");
            return;
        }

        if (!sellerId) {
            alert("Please select a seller!");
            return;
        }

       
        // STEP 3: Extract Order Details
        const detailRows = document.querySelectorAll("#editDetailsTableBody tr");

        if (detailRows.length === 0) {
            alert("Please add at least one order detail!");
            return;
        }

        const orderDetails = [];
        let hasError = false;

        detailRows.forEach((row, index) => {
            const productId = row.querySelector(".product-id").value;  
            const unitPrice = Number(row.querySelector(".unit-price").value) || 0;
            const quantity = Number(row.querySelector(".quantity").value) || 0;

            console.log(`📦 Row ${index + 1}:`, { productId, unitPrice, quantity });

            if (!productId) {
                alert(`Please select a product for row ${index + 1}!`);
                hasError = true;
                return;
            }

            if (quantity <= 0) {
                alert(`Quantity in row ${index + 1} must be greater than zero!`);
                hasError = true;
                return;
            }

            orderDetails.push({
                productId: productId,     
                unitPrice: unitPrice,
                quantity: quantity
            });
        });

        if (hasError) return;

        
        // STEP 4: Calculate Final Price
       const finalPrice = orderDetails.reduce(
            (sum, detail) => sum + (detail.unitPrice * detail.quantity), 0
        );
        console.log("💰 Final Price:", finalPrice);

       
        // STEP 5: Build DTO 
        
        const updateDto = {
            id: orderId,                   
            guidKey: orderGuidKey,         
            customerId: customerId,       
            sellerId: sellerId,           
            orderDetails: orderDetails
        };

        console.log("📤 Sending data:", JSON.stringify(updateDto, null, 2));

       
        // STEP 6: Send PUT Request
       
        const response = await fetch(`/Order/PutOrder`, { 
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            },
            body: JSON.stringify(updateDto)
        });

    
        // STEP 7: Read Response
        let responseData;
        const contentType = response.headers.get("content-type");

        if (contentType && contentType.includes("application/json")) {
            responseData = await response.json();
        } else {
            responseData = await response.text();
        }

        console.log("Server response:", responseData);


        // STEP 8: Check Response
          if (!response.ok) {
            // نمایش خطاهای اعتبارسنجی
            if (responseData && typeof responseData === 'object') {
                const errors = [];
                for (const key in responseData) {
                    if (Array.isArray(responseData[key])) {
                        errors.push(`${key}: ${responseData[key].join(', ')}`);
                    }
                }
                if (errors.length > 0) {
                    alert(`Validation errors:\n${errors.join('\n')}`);
                } else {
                    alert(`Error: ${JSON.stringify(responseData)}`);
                }
            } else {
                alert(`Error updating order: ${responseData}`);
            }
            return;
        }

      
        // STEP 9: Success
        alert("Order updated successfully!");

        const modal = bootstrap.Modal.getInstance(document.getElementById("editOrderModal"));
        if (modal) {
            modal.hide();
        }

        await loadOrders();
        console.log("Update completed successfully");

    } catch (error) {
        console.error("Error in updateOrder:", error);
        alert(`Error updating order: ${error.message}`);
    }
};




// *** Event Listeners


// ----- Modal Events (set isEditMode) -----
createOrderModal.addEventListener("show.bs.modal", function () {
    isEditMode = false;
});

editOrderModal.addEventListener("show.bs.modal", function () {
    isEditMode = true;
});

// ----- Create Form Events -----

// Add new detail row
addDetailBtn.addEventListener("click", () => {
    const options = products.map(p =>
        `<option value="${p.id}">${p.productName}</option>`
    ).join("");

    detailsTableBody.insertAdjacentHTML(
        "beforeend",
        `
        <tr>
            <td>
                <select class="form-select product-id">
                    <option value="">Select product...</option>
                    ${options}
                </select>
            </td>
            <td>
                <input type="number" class="form-control unit-price">
            </td>
            <td>
                <input type="number" class="form-control quantity" min="1" value="1">
            </td>
            <td>
                <input type="number" class="form-control total-price" readonly>
            </td>
            <td>
                <button type="button" class="btn btn-danger remove-detail">
                    Delete
                </button>
            </td>
        </tr>
        `
    );
});

// Product selection change (create form)
detailsTableBody.addEventListener("change", function (e) {
    if (!e.target.classList.contains("product-id")) return;

    const currentSelect = e.target;
    const productId = currentSelect.value;

    // Check for duplicate product
    const isDuplicate = [...document.querySelectorAll(".product-id")]
        .filter(x => x !== currentSelect)
        .some(x => x.value === productId);

    if (isDuplicate) {
        alert("This product has already been selected in the order.");
        currentSelect.value = "";
        const row = currentSelect.closest("tr");
        row.querySelector(".unit-price").value = "";
        calculateRowTotal(row);
        return;
    }

    const product = products.find(p => p.id == productId);
    if (!product) return;

    const row = currentSelect.closest("tr");
    row.querySelector(".unit-price").value = product.unitPrice;
    calculateRowTotal(row);
});

// Quantity change (create form)
detailsTableBody.addEventListener("input", function (e) {
    if (!e.target.classList.contains("quantity")) return;
    const row = e.target.closest("tr");
    calculateRowTotal(row);
});

// Delete detail row (create form)
detailsTableBody.addEventListener("click", function (e) {
    if (!e.target.classList.contains("remove-detail")) return;
    e.target.closest("tr").remove();
    calculateFinalPrice();
});

// Create order button
createBtn.addEventListener("click", createOrder);

// ================================================================
// *** Edit Form Events
// ================================================================

// Add new detail row in edit
editAddDetailBtn.addEventListener("click", () => {
    addEditDetailRow({
        productId: "",
        unitPrice: 0,
        quantity: 1,
        totalPrice: 0
    });
});

// Product selection change (edit form)
editDetailsTableBody.addEventListener("change", function (e) {
    if (!e.target.classList.contains("product-id")) return;

    const currentSelect = e.target;
    const productId = currentSelect.value;

    // Check for duplicate product
    const isDuplicate = [...editDetailsTableBody.querySelectorAll(".product-id")]
        .filter(x => x !== currentSelect)
        .some(x => x.value === productId);

    if (isDuplicate) {
        alert("This product has already been selected in the order.");
        currentSelect.value = "";
        const row = currentSelect.closest("tr");
        row.querySelector(".unit-price").value = "";
        row.querySelector(".total-price").value = "";
        calculateEditFinalPrice();
        return;
    }

    const product = products.find(p => p.id == productId);
    if (!product) return;

    const row = currentSelect.closest("tr");
    row.querySelector(".unit-price").value = product.unitPrice;
    calculateEditRowTotal(row);
});

// Edit Form - Input Events (Combined)
// This single event handler manages both unit-price and quantity changes
// It recalculates the row total and updates the final price in real-time
editDetailsTableBody.addEventListener("input", function (e) {
    const target = e.target;

    // Check if the changed field is either unit-price or quantity
    if (target.classList.contains("unit-price") ||
        target.classList.contains("quantity")) {

        // Find the parent row
        const row = target.closest("tr");

        // Recalculate row total (unitPrice * quantity)
        calculateEditRowTotal(row);
    }
});

// Edit Form - Delete Detail Row
// Removes the selected detail row and updates the final price
editDetailsTableBody.addEventListener("click", function (e) {
    // Check if the clicked element is a delete button
    if (!e.target.classList.contains("remove-detail")) return;

    // Find and remove the parent row
    const row = e.target.closest("tr");
    if (row) {
        row.remove(); // Remove row from DOM
        calculateEditFinalPrice(); // Update final price after deletion
    }
});

// Click event for update order button
updateOrderBtn.addEventListener("click", updateOrder);

// ================================================================
// *** General Events (Event Delegation)
// ================================================================

// Click on edit order buttons
document.addEventListener("click", function (e) {
    const btn = e.target.closest(".edit-order");
    if (!btn) return;

    const orderId = btn.dataset.id;
    const order = orders.find(x => x.id == orderId);
    if (!order) return;

    fillEditModal(order);
});

// Click on select customer buttons
document.addEventListener("click", function (e) {
    const btn = e.target.closest(".select-customer");
    if (!btn) return;

    const customerIdValue = btn.dataset.id;
    const firstName = btn.dataset.name;
    const lastName = btn.dataset.lastname;

    if (isEditMode) {
        editCustomerId.value = customerIdValue;
        editCustomerFirstName.value = firstName;
        editCustomerLastName.value = lastName;
    } else {
        customerId.value = customerIdValue;
        customerFirstName.value = firstName;
        customerLastName.value = lastName;
    }

    // Close customer modal
    bootstrap.Modal.getInstance(customerModal).hide();

    // Show appropriate modal again
    if (isEditMode) {
        bootstrap.Modal.getOrCreateInstance(editOrderModal).show();
    } else {
        bootstrap.Modal.getOrCreateInstance(createOrderModal).show();
    }
});

// Click on select seller buttons
document.addEventListener("click", function (e) {
    const btn = e.target.closest(".select-seller");
    if (!btn) return;

    const sellerIdValue = btn.dataset.id;
    const firstName = btn.dataset.firstname;
    const lastName = btn.dataset.lastname;

    if (isEditMode) {
        editSellerId.value = sellerIdValue;
        editSellerFirstName.value = firstName;
        editSellerLastName.value = lastName;
    } else {
        sellerId.value = sellerIdValue;
        sellerFirstName.value = firstName;
        sellerLastName.value = lastName;
    }

    // Close seller modal
    bootstrap.Modal.getInstance(sellerModal).hide();

    // Show appropriate modal again
    if (isEditMode) {
        bootstrap.Modal.getOrCreateInstance(editOrderModal).show();
    } else {
        bootstrap.Modal.getOrCreateInstance(createOrderModal).show();
    }
});


// *** Initial Page Load

window.addEventListener("load", async () => {
    await fetchCustomers();
    await fetchSellers();
    await fetchProducts();
    await loadOrders();
});