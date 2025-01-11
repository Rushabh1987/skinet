import { Component, inject } from '@angular/core';
import { CartService } from '../../core/services/cart.service';
import { CartItemComponent } from './cart-item/cart-item.component';
import { OrderSummeryComponent } from '../../shared/components/order-summery/order-summery.component';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CartItemComponent, OrderSummeryComponent],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.scss',
})
export class CartComponent {
  cartService = inject(CartService);
}
