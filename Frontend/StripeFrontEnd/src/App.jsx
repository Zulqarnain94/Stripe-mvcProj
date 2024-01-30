import React from "react";
import { Elements } from "@stripe/react-stripe-js";
import { loadStripe } from "@stripe/stripe-js";
import CheckoutForm from "./Components/CheckoutForm";

const stripePromise = loadStripe(
  "pk_test_51OdthoBtLyUDk5IywgHBe06AJYc1cuidNqi1FqAX6aUg9aZKfzkmYn3XodjGpeeP5eKvY1zexOJoSh8FFAisLG5i00cGFmfZJL"
);

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <h1>Stripe Payment Integration</h1>
        <Elements stripe={stripePromise}>
          <CheckoutForm />
        </Elements>
      </header>
    </div>
  );
}

export default App;
