import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { NgIf, TitleCasePipe } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { RouterLink, RouterLinkActive,Router } from '@angular/router';
import { Toast, ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule,BsDropdownModule,RouterLink,RouterLinkActive,TitleCasePipe],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  accountService = inject(AccountService);
  private router = inject(Router);
  private toastr = inject(ToastrService);
  model: any = {};

  login(){
    this.accountService.login(this.model).subscribe({
      next: _=>{

        this.router.navigateByUrl('/members')

      },
      error: error => this.toastr.error(error.error)
    })
  }
  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/')

  }
}
