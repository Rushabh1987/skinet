import { Component, inject } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { RouterLink } from '@angular/router';
import { CartService } from '../../../core/services/cart.service';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-order-summery',
  standalone: true,
  imports: [
    MatButton,
    RouterLink,
    MatFormField,
    MatLabel,
    MatInput,
    CurrencyPipe,
  ],
  templateUrl: './order-summery.component.html',
  styleUrl: './order-summery.component.scss',
})
export class OrderSummeryComponent {
  cartService = inject(CartService);
}
